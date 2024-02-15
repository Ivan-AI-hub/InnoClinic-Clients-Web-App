using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ClientsWebApp.Blazor.Pages.Conference
{
    public partial class Rtc 
    {
        private IJSObjectReference? _module;
        private bool _callDisabled;
        private bool _hangupDisabled = true;

        [SupplyParameterFromQuery]
        [Parameter]
        public string Channel { get; set; }

        [Inject] NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (string.IsNullOrWhiteSpace(Channel))
            {
                NavigationManager.NavigateTo("/");
            }

            RtcService.Initialize(Channel);
            RtcService.OnRemoteStreamAcquired += RtcOnOnRemoteStreamAcquired;

            var stream = await RtcService.StartLocalStream();
            await SetLocalStream(stream);

            await RtcService.Join();

        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _module = await Js.InvokeAsync<IJSObjectReference>("import", "./js/xyz.js");
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async void RtcOnOnRemoteStreamAcquired(object? _, IJSObjectReference e)
        {
            await _module.InvokeVoidAsync("setRemoteStream", e);

            _callDisabled = true;
            _hangupDisabled = false;
            StateHasChanged();
        }

        private async Task CallAction()
        {
            _callDisabled = true;
            await RtcService.Call();
            _hangupDisabled = false;
        }
        private async Task HangupAction()
        {
            await RtcService.Hangup();
            _callDisabled = false;
            _hangupDisabled = true;
        }

        private async Task SetLocalStream(IJSObjectReference stream)
        {
            await _module.InvokeVoidAsync("setLocalStream", stream);
        }

        public async ValueTask DisposeAsync()
        {
            _module?.DisposeAsync();
            await RtcService.Hangup();
        }
    }
}