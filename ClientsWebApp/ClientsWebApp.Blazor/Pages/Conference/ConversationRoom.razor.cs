using Blazored.Modal.Services;
using Blazored.Modal;
using BlazorRtc.Client.WebRtc;
using ClientsWebApp.Blazor.Components.Modals;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Conference
{
    public partial class ConversationRoom
    {
        private bool _isConnectError;

        private ElementReference _callLocalVideoStream;
        private ElementReference _remoteVideoStream;

        [SupplyParameterFromQuery]
        [Parameter]
        public string Channel { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; }
        [Inject] WebRtcService RtcService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (string.IsNullOrWhiteSpace(Channel))
            {
                NavigationManager.NavigateTo("/");
            }
            NavigationManager.LocationChanged += async (e, h) => await DisposeAsync();
            RtcService.OnRemoteVideoLeave += RemoteStreamLeave;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!firstRender)
            {
                return;
            }

            await RtcService.InitializeAsync(Channel, _callLocalVideoStream, _remoteVideoStream);

            await RtcService.StartLocalStream();

            await Join();
        }
        private async Task Join()
        {
            var successJoin = await RtcService.Join();
            if (!successJoin)
            {
                _isConnectError = true;
                StateHasChanged();
                return;
            }

            await RtcService.Call();
        }

        private void HangUp()
        {
            NavigationManager.NavigateTo($"/conference/prepare?Channel={Channel}");
        }

        private void RemoteStreamLeave()
        {
            var options = new ModalOptions()
            {
                HideCloseButton = true,
                DisableBackgroundCancel = false
            };

            var modal = Modal.Show<UserLeaveMeetingRoomModal>("Oh, no", options);
        }

        public async ValueTask DisposeAsync()
        {
            await RtcService.Hangup();
            await RtcService.StopLocalStream();
            await RtcService.DisposeAsync();
        }
    }
}
