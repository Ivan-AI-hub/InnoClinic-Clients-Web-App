﻿"use strict";
// Set up media stream constant and parameters.
const mediaStreamConstraints = {
	video: true
	//audio: true
};

// Set up to exchange only video.
const offerOptions = {
	offerToReceiveVideo: 1
	//offerToReceiveAudio: 1
};

const servers = {
	iceServers: [
		{ urls: 'stun:stun.1.google.com:19302' },
		{ urls: 'stun:stun1.l.google.com:19302' }
	]
}

let dotNet;
let localVideo;
let remoteVideo
let localStream;
let remoteStream;
let peerConnection;

let isOffering;
let isOffered;

export function initialize(dotNetRef, localVideoRef, remoteVideoRef) {
	dotNet = dotNetRef;
	localVideo = localVideoRef;
	remoteVideo = remoteVideoRef;
}
export async function startLocalStream() {
	console.log("Requesting local stream.");
	localStream = await navigator.mediaDevices.getUserMedia(mediaStreamConstraints);
	localVideo.srcObject = localStream
	localVideo.muted = 'muted';

	//remoteVideo.srcObject = localStream
	//remoteVideo.muted = 'muted';
}

export async function stopLocalStream() {
	if (localStream && localStream.active) {
		localStream.getTracks().forEach((track) => { track.enabled = false });
		dotNet.invokeMethodAsync("SendCameraOff");
	}
}

export function playLocalStream() {
	if (localStream) {
		localVideo.play();
		//localStream.getAudioTracks().forEach(function (track) {
		//	track.enabled = false
		//})
		localStream.getVideoTracks().forEach(function (track) {
			track.enabled = true
		})

		dotNet.invokeMethodAsync("SendCameraOn");
	}
}

export function stopMicrophone() {
	if (localStream) {
		localStream.getAudioTracks().forEach(function (track) {
			track.enabled = false
		})
	}
}
export function playMicrophone() {
	if (localStream) {
		localStream.getAudioTracks().forEach(function (track) {
			track.enabled = true
		})
	}
}


function createPeerConnection() {
	if (peerConnection != null) return;
	// Create peer connections and add behavior.
	peerConnection = "hello";
	peerConnection = new RTCPeerConnection(servers);
	console.log("Created local peer connection object peerConnection.");

	peerConnection.addEventListener("icecandidate", handleConnection);
	peerConnection.addEventListener("iceconnectionstatechange", handleConnectionChange);
	peerConnection.addEventListener("addstream", gotRemoteMediaStream);

	// Add local stream to connection and create offer to connect.
	peerConnection.addStream(localStream);
	console.log("Added local stream to peerConnection.");
}

// first flow: This client initiates call. Sequence is:
// Create offer - send to peer - receive answer - set stream
// Handles call button action: creates peer connection.
export async function callAction() {
	if (isOffered) return Promise.resolve();

	isOffering = true;
	console.log("Starting call.");
	createPeerConnection();

	console.log("peerConnection createOffer start.");
	let offerDescription = await peerConnection.createOffer(offerOptions);
	console.log(`Offer from peerConnection:\n${offerDescription.sdp}`);
	console.log("peerConnection setLocalDescription start.");
	await peerConnection.setLocalDescription(offerDescription);
	console.log("peerConnection.setLocalDescription(offerDescription) success");
	return JSON.stringify(offerDescription);
}

// Signaling calls this once an answer has arrived from other peer. Once
// setRemoteDescription is called, the addStream event trigger on the connection.
export async function processAnswer(descriptionText) {
	let description = JSON.parse(descriptionText);
	console.log("processAnswer: peerConnection setRemoteDescription start.");
	await peerConnection.setRemoteDescription(description);
	console.log("peerConnection.setRemoteDescription(description) success");
}

// In this flow, the user gets an offer from signaling from a peer.
// In this case, we setRemoteDescription similar to when we got the answer
// in the flow above. srd triggers addStream.
export async function processOffer(descriptionText) {
	console.log("processOffer");
	if (isOffering) return;

	createPeerConnection();
	let description = JSON.parse(descriptionText);
	console.log("peerConnection setRemoteDescription start.");
	await peerConnection.setRemoteDescription(description);

	console.log("peerConnection createAnswer start.");
	let answer = await peerConnection.createAnswer();
	console.log(`Answer: ${answer.sdp}.`);
	console.log("peerConnection setLocalDescription start.");
	await peerConnection.setLocalDescription(answer);

	console.log("dotNet SendAnswer.");
	dotNet.invokeMethodAsync("SendAnswer", JSON.stringify(answer));
}

export async function processCandidate(candidateText) {
	let candidate = JSON.parse(candidateText);
	console.log("processCandidate: peerConnection addIceCandidate start.");
	await peerConnection.addIceCandidate(candidate);
	console.log("addIceCandidate added.");
}

// Handles hangup action: ends up call, closes connections and resets peers.
export function hangupAction() {
	if (peerConnection) {
		peerConnection.close();
		peerConnection = null;
	}
	if (remoteStream && remoteStream.active) {
		remoteStream.getTracks().forEach((track) => { track.stop(); });
	}
	if (localStream && localStream.active) {
		localStream.getTracks().forEach((track) => { track.stop(); });
	}
	console.log("Ending call.");
	isOffering = false;
	isOffered = false;
}

export function removePeerLeft() {

	if (peerConnection) {
		peerConnection.close();
		peerConnection = null;
	}
	if (remoteStream && remoteStream.active) {
		remoteStream.getTracks().forEach((track) => { track.stop(); });
		remoteVideo.srcObject = null
	}

	isOffering = false;
	isOffered = false;
}
// Handles remote MediaStream success by handing the stream to the blazor component.
async function gotRemoteMediaStream(event) {
	const mediaStream = event.stream;
	console.log(mediaStream);
	remoteStream = mediaStream;
	remoteVideo.srcObject = remoteStream
	console.log("Remote peer connection received remote stream.");
}
export function getRemoteStream() {
	return remoteStream;
}

// Sends candidates to peer through signaling.
async function handleConnection(event) {
	const iceCandidate = event.candidate;

	if (iceCandidate) {
		await dotNet.invokeMethodAsync("SendCandidate", JSON.stringify(iceCandidate));

		console.log(`peerConnection ICE candidate:${event.candidate.candidate}.`);
	}
}

// Logs changes to the connection state.
async function handleConnectionChange(event) {
	const peerConnection = event.target;
	console.log("ICE state change event: ", event);
	console.log(`peerConnection ICE state: ${peerConnection.iceConnectionState}.`);
	console.log(`new state: ${peerConnection.connectionState}.`);
	console.log('connection', peerConnection);
	await dotNet.invokeMethodAsync("StateChanged", peerConnection.iceConnectionState);
}

