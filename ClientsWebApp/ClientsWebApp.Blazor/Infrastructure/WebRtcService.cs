using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Domain.Identity.Tokens;
using ClientsWebApp.Services.Settings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Linq.Expressions;

namespace BlazorRtc.Client.WebRtc;

public class WebRtcService : IAsyncDisposable
{
    private string _videoServiceUrl;
    private readonly IJSRuntime _jsRuntime;
    private readonly IStorageService _storageService;

    private Lazy<IJSObjectReference> _rtcJsRef = new();
    private HubConnection? _hub;
    private string _channel;

    public event Action OnRemoteVideoLeave;
    public bool HasRemoteUser { get; private set; }

    public WebRtcService(IJSRuntime jsRuntime, IOptions<ServicesUriSettings> settings, IStorageService storageService)
    {
        _jsRuntime = jsRuntime;

        _videoServiceUrl = settings.Value.VideoHubUrl;
        _storageService = storageService;
    }

    public async Task InitializeAsync(string channel, ElementReference localVideo, ElementReference remoteVideo)
    {
        _channel = channel;
        var rtcService = DotNetObjectReference.Create(this);
        await WaitForReference();

        await _rtcJsRef.Value.InvokeVoidAsync("initialize", rtcService, localVideo, remoteVideo);
    }
    public async Task<bool> Join()
    {
        var hub = await GetHub();
        var success = await hub.InvokeAsync<bool>("join", _channel);

        return success;
    }

    public async Task StartLocalStream()
    {
        await WaitForReference();

        await _rtcJsRef.Value.InvokeVoidAsync("startLocalStream");
    }
    public async Task StopLocalStream()
    {
        await WaitForReference();

        await _rtcJsRef.Value.InvokeVoidAsync("stopLocalStream");
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

        var token = await _storageService.GetAsync<SecurityToken>(nameof(SecurityToken));
        var hub = new HubConnectionBuilder()
            .WithUrl(_videoServiceUrl, options =>
            {
                options.AccessTokenProvider = async () =>
                {
                    var token = await _storageService.GetAsync<SecurityToken>(nameof(SecurityToken));
                    return token.AccessToken;
                };
                options.Headers.Add("ngrok-skip-browser-warning", "yes");
                //options.SkipNegotiation = true;
                //options.Transports = HttpTransportType.WebSockets;
            })
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

        hub.On<string>("Join", async (connectionId) =>
        {
            HasRemoteUser = true;
            await Call();
        });

        hub.On<string>("Leave", async (connectionId) =>
        {
            HasRemoteUser = false;
            await _rtcJsRef.Value.InvokeVoidAsync("removePeerLeft");
            OnRemoteVideoLeave?.Invoke();
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

    private async Task WaitForReference()
    {
        if (_rtcJsRef.IsValueCreated is false)
        {
            _rtcJsRef = new(await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "/js/WebRtcService.cs.js"));
        }
    }

    public async ValueTask DisposeAsync()
    {
        await StopLocalStream();
        await Hangup();
        await _rtcJsRef.Value.DisposeAsync();
        await _hub.DisposeAsync();
    }
}