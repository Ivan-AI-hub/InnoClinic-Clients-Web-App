using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Conference
{
    public partial class Rtc : IAsyncDisposable
    {
        private bool _callDisabled;
        private bool _hangupDisabled = true;

        private ElementReference _remoteVideoStream;
        private ElementReference _localVideoStream;

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


        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await RtcService.Join(_localVideoStream, _remoteVideoStream);

                await RtcService.StartLocalStream();

            }
            await base.OnAfterRenderAsync(firstRender);
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

        public async ValueTask DisposeAsync()
        {
            await RtcService.DisposeAsync();
        }
    }
}