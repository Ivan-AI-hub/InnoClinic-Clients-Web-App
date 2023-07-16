using ClientsWebApp.Domain.Profiles.Receptionist;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Profiles.Receptionists
{
    [Authorize(Roles = "Admin")]
    public partial class ReceptionistHome : CancellableComponent
    {
        [Inject] AuthenticationStateHelper StateHelper { get; set; }
        [Inject] IReceptionistService ReceptionistService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private Receptionist? Receptionist { get; set; }
        private bool IsLoading { get; set; } = true;
        protected override async void OnInitialized()
        {
            IsLoading = true;
            var email = await StateHelper.GetEmailAsync();
            try
            {
                Receptionist = await ReceptionistService.GetByEmailAsync(email, _cts.Token);
            }
            catch
            {
                NavigationManager.NavigateTo("/receptionists/create");
            }
            IsLoading = false;
            StateHasChanged();
        }

        private void NavigateToDeletePage()
        {
            NavigationManager.NavigateTo("/receptionists/delete");
        }

        private void NavigateToEditPage()
        {
            NavigationManager.NavigateTo("/receptionists/edit");
        }
    }
}
