using ClientsWebApp.Application.Models.Enums;
using ClientsWebApp.Domain.Images;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace ClientsWebApp.Blazor.Pages.Conference
{
    public partial class VideoElement
    {
        [Inject] public ISnackbar Snackbar { get; set; }
        [Parameter] public WebRtcConnectionState ConnectionState { get; set; }
        [Parameter] public Image? LocalPoster  { get; set; }
        [Parameter] public Image? RemotePoster { get; set; }
        [Parameter] public bool ShowLocalPoster { get; set; }
        [Parameter] public bool ShowRemotePoster { get; set; }
        [Parameter] public ElementReference LocalStream { get; set; }
        [Parameter] public ElementReference RemoteStream { get; set; }

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