using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Receptionists;
using ClientsWebApp.Blazor.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Receptionists
{
    [Authorize(Roles = "Admin")]
    public partial class ReceptionistHome : CancellableComponent
    {
        [Inject] AuthenticationStateHelper StateHelper { get; set; }
        [Inject] IReceptionistManager ReceptionistManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private ReceptionistDTO? Receptionist { get; set; }
        private bool IsLoading { get; set; } = true;
        protected override async void OnInitialized()
        {
            IsLoading = true;
            var email = await StateHelper.GetEmailAsync();
            try
            {
                Receptionist = await ReceptionistManager.GetByEmailAsync(email, _cts.Token);
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
