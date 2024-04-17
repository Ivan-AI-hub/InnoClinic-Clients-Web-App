using Blazored.Modal.Services;
using BlazorRtc.Client.WebRtc;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Conference
{
    public partial class PrepareRoom : IAsyncDisposable
    {
        private bool _joinDisabled;

        private ElementReference _preparedLocalVideoStream;

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
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!firstRender)
            {
                return;
            }

            await RtcService.InitializeAsync(Channel, _preparedLocalVideoStream, default);

            await RtcService.StartLocalStream();
        }
        private async Task Join()
        {
            NavigationManager.NavigateTo($"/conference?Channel={Channel}");
        }

        public async ValueTask DisposeAsync()
        {
            await RtcService.DisposeAsync();
        }
    }
}
