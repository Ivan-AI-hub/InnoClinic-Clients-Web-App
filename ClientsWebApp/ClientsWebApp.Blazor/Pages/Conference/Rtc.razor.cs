using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Conference
{
    public partial class Rtc : IAsyncDisposable
    {
        private bool _successefullJoin;
        private bool _joinDisabled;
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

            RtcService.OnRemoteVideoLeave += RemoteStreamLeave;
        }

        private async Task Join()
        {
            await RtcService.InitializeAsync(Channel, _localVideoStream, _remoteVideoStream);
            _successefullJoin = await RtcService.Join();
            if (!_successefullJoin)
            {
                return;
            }

            await RtcService.StartLocalStream();
            _hangupDisabled = false;
            _joinDisabled = true;
        }
        private async Task HangupAction()
        {
            await RtcService.Hangup();

            await Task.Delay(1000);

            _joinDisabled = false;
            _hangupDisabled = true;
        }

        private void RemoteStreamLeave()
        {
            Console.WriteLine("Remote user leave");
            
        }

        public async ValueTask DisposeAsync()
        {
            await RtcService.DisposeAsync();
        }
    }
}