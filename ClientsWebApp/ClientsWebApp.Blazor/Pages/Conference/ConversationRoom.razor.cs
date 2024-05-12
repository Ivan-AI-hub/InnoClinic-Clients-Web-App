using Blazored.Modal.Services;
using Blazored.Modal;
using BlazorRtc.Client.WebRtc;
using ClientsWebApp.Blazor.Components.Modals;
using Microsoft.AspNetCore.Components;
using ClientsWebApp.Application.Models.Enums;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Domain.Images;
using MudBlazor;
using ClientsWebApp.Domain.Appointments;

namespace ClientsWebApp.Blazor.Pages.Conference
{
    public partial class ConversationRoom : CancellableComponent
    {
        private bool _showLocalPoster;
        private bool _showRemotePoster;

        private bool _isConnectError;
        private bool _micEnabled = true;
        private bool _videoEnabled = true;
        private WebRtcConnectionState _state;

        private ElementReference _localVideoStream;
        private ElementReference _remoteVideoStream;

        public Image? _localPoster;
        public Image? _remotePoster;

        [SupplyParameterFromQuery]
        [Parameter]
        public string Channel { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] WebRtcService RtcService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] public IAppointmentManager AppointmentManager { get; set; }
        [Inject] AuthenticationStateHelper StateHelper { get; set; }
        [Inject] IPatientManager PatientManager { get; set; }
        [Inject] IDoctorManager DoctorManager{ get; set; }


        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (string.IsNullOrWhiteSpace(Channel))
            {
                NavigationManager.NavigateTo("/");
            }
            
            NavigationManager.LocationChanged += async (e, h) => await DisposeAsync();
            RtcService.OnRemoteVideoLeave += RemoteStreamLeave;
            RtcService.OnStateChanged += ConnectionStateChanged;
            RtcService.OnRemoteStreamPaused += RemoteStreamPaused;
            RtcService.OnRemoteStreamStarted += RemoteStreamStarted;

            if (!Guid.TryParse(Channel, out var channelId))
            {
                NavigationManager.NavigateTo("/");
            }
            var appointment = await AppointmentManager.GetByIdAsync(channelId, _cts.Token);
            if (appointment is null)
            {
                NavigationManager.NavigateTo("/");
                return;
            }

            var role = await StateHelper.GetRoleAsync();

            var patient = await PatientManager.GetByIdAsync(appointment.Patient.Id, _cts.Token);
            var doctor = await DoctorManager.GetByIdAsync(appointment.Doctor.Id, _cts.Token);

            _localPoster = role == nameof(Role.Patient)
                ? patient.Info.Photo is not null ? await patient.Info.Photo : null
                : doctor.Info.Photo is not null ? await doctor.Info.Photo : null;

            _remotePoster = role == nameof(Role.Patient)
                ? doctor.Info.Photo is not null ? await doctor.Info.Photo : null
                : patient.Info.Photo is not null ? await patient.Info.Photo : null;

        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!firstRender)
            {
                return;
            }

            await RtcService.InitializeAsync(Channel, _localVideoStream, _remoteVideoStream);

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
        }

        private void HangUp()
        {
            NavigationManager.NavigateTo($"/conference/prepare?Channel={Channel}");
        }

        private async Task MicSettingChangeAsync()
        {
            _micEnabled = !_micEnabled;
            if (_micEnabled)
            {
                await RtcService.StartMicrophone();
            }
            else
            {
                await RtcService.StopMicrophone();
            }
            StateHasChanged();
        }

        private async void VideoStreamSettingChange()
        {
            _videoEnabled = !_videoEnabled;

            if (_videoEnabled)
            {
                await RtcService.StartLocalStream();
                _showLocalPoster = false;
            }
            else
            {
                await RtcService.StopLocalStream();
                _showLocalPoster = true;
            }
            StateHasChanged();
        }

        private void RemoteStreamLeave()
        {
            var options = new ModalOptions()
            {
                HideCloseButton = true,
                DisableBackgroundCancel = false
            };

            var modal = Modal.Show<UserLeaveMeetingRoomModal>("Oh, no", options);
            ConnectionStateChanged(WebRtcConnectionState.Closed);
        }
        private void ConnectionStateChanged(WebRtcConnectionState state)
        {
            Console.WriteLine($"State: {state}");
            _state = state;
            StateHasChanged();
        }

        private void RemoteStreamPaused()
        {
            _showRemotePoster = true;
            StateHasChanged();
        }

        private void RemoteStreamStarted()
        {
            _showRemotePoster = false;
            StateHasChanged();
        }

        public async ValueTask DisposeAsync()
        {
            await RtcService.DisposeAsync();
        }

        public void OnRemoteStreamPlaying()
        {
            Snackbar.Add("[Remote Stream] play");
        }

        public void OnRemoteStreamStop()
        {
            Snackbar.Add("[Remote Stream] stop");
        }

        public void OnLocalStreamPlaying()
        {
            Snackbar.Add("[Local Stream] play");
        }

        public void OnLocalStreamStop()
        {
            Snackbar.Add("[Local Stream] stop");
        }
    }
}
