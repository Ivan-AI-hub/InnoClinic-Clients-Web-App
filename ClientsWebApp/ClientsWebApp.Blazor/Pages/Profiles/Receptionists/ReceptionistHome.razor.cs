using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Receptionists;
using ClientsWebApp.Blazor.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Blazored.Modal.Services;
using Blazored.Modal;

namespace ClientsWebApp.Blazor.Pages.Profiles.Receptionists
{
    [Authorize(Roles = "Admin")]
    public partial class ReceptionistHome : CancellableComponent
    {
        [CascadingParameter] public IModalService Modal { get; set; }
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
                NavigateToCreatePage();
            }
            IsLoading = false;
            StateHasChanged();
        }

        private void NavigateToDeletePage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large,
                DisableBackgroundCancel = true
            };
            Modal.Show<DeleteReceptionist>("Delete profile", options);
            //NavigationManager.NavigateTo("/patients/delete");
        }

        private void NavigateToCreatePage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large,
                DisableBackgroundCancel = true
            };
            Modal.Show<CreateReceptionist>("Create profile", options);
            //NavigationManager.NavigateTo("/patients/create");
        }
        private void NavigateToEditPage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large,
                DisableBackgroundCancel = true
            };
            Modal.Show<EditReceptionist>("Edit profile", options);
            //NavigationManager.NavigateTo("/patients/edit");
        }
    }
}
