using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace BlazorRtc.Client.WebRtc;

public class WebRtcService
{
    private string _videoServiceUrl;
    private readonly IJSRuntime _jsRuntime;
    private Lazy<IJSObjectReference> _rtcJsRef = new();
    private HubConnection? _hub;
    private string _channel;

    public event EventHandler<IJSObjectReference>? OnRemoteStreamAcquired;

    public WebRtcService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        
        _videoServiceUrl = "https://localhost:7244/messagetesthub";
    }

    public void Initialize(string channel)
    {
        _channel = channel;

    }
    public async Task Join()
    {
        var hub = await GetHub();
        await hub.SendAsync("join", _channel);
  
        var rtcService = DotNetObjectReference.Create(this);
        await WaitForReference();

        await _rtcJsRef.Value.InvokeVoidAsync("initialize", rtcService);
    }

    public async Task<IJSObjectReference> StartLocalStream()
    {
        await WaitForReference();

        var stream = await _rtcJsRef.Value.InvokeAsync<IJSObjectReference>("startLocalStream");

        return stream;
    }

    public async Task Call()
    {
        await WaitForReference();

        var offerDescription = await _rtcJsRef.Value.InvokeAsync<string>("callAction");

        await SendOffer(offerDescription);
    }

    public async Task Hangup()
    {
        await WaitForReference();
        await _rtcJsRef.Value.InvokeVoidAsync("hangupAction");

        var hub = await GetHub();

        await hub.SendAsync("leave", _channel);
    }

    private async Task<HubConnection> GetHub()
    {
        if (_hub is not null) return _hub;

        var hub = new HubConnectionBuilder()
            .WithUrl(_videoServiceUrl)
            .Build();

        hub.On<string, string, string>("SignalWebRtc", async (signalingChannel, type, payload) =>
        {
            await WaitForReference();

            switch (type)
            {
                case "offer":
                    await _rtcJsRef.Value.InvokeVoidAsync("processOffer", payload);
                    break;
                case "answer":
                    await _rtcJsRef.Value.InvokeVoidAsync("processAnswer", payload);
                    break;
                case "candidate":
                    await _rtcJsRef.Value.InvokeVoidAsync("processCandidate", payload);
                    break;
            }
        });

        await hub.StartAsync();
        _hub = hub;
        return _hub;
    }

    [JSInvokable]
    public async Task SendOffer(string offer)
    {
        var hub = await GetHub();
        await hub.SendAsync("SignalWebRtc", _channel, "offer", offer);
    }

    [JSInvokable]
    public async Task SendAnswer(string answer)
    {
        var hub = await GetHub();
        await hub.SendAsync("SignalWebRtc", _channel, "answer", answer);
    }

    [JSInvokable]
    public async Task SendCandidate(string candidate)
    {
        var hub = await GetHub();
        await hub.SendAsync("SignalWebRtc", _channel, "candidate", candidate);
    }    

    [JSInvokable]
    public async Task SetRemoteStream()
    {
        await WaitForReference();
        var stream = await _rtcJsRef.Value.InvokeAsync<IJSObjectReference>("getRemoteStream");

        OnRemoteStreamAcquired?.Invoke(this, stream);
    }

    private async Task WaitForReference()
    {
        if (_rtcJsRef.IsValueCreated is false)
        {
           _rtcJsRef = new(await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/WebRtcService.cs.js"));
        }
    }
}