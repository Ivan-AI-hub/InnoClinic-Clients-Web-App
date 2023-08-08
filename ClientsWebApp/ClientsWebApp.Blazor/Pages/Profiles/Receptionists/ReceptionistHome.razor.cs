using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Receptionists;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

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
        protected override async void OnInitialized()
        {
            var email = await StateHelper.GetEmailAsync();
            try
            {
                Receptionist = await ReceptionistManager.GetByEmailAsync(email, _cts.Token);
            }
            catch (NotFoundException ex)
            {
                NavigateToCreatePage();
            }
            StateHasChanged();
        }

        private void NavigateToDeletePage()
        {
            Modal.Show<DeleteReceptionist>("Delete profile");
            //NavigationManager.NavigateTo("/patients/delete");
        }

        private void NavigateToCreatePage()
        {
            var options = new ModalOptions() 
            {
                Size = ModalSize.Large
            };
            Modal.Show<CreateReceptionist>("Create profile", options);
            //NavigationManager.NavigateTo("/patients/create");
        }
        private void NavigateToEditPage()
        {
            var options = new ModalOptions() 
            {
                Size = ModalSize.Large
            };
            var parameters = new ModalParameters();
            parameters.Add(nameof(EditReceptionist.OldReceptionist), Receptionist);
            Modal.Show<EditReceptionist>("Edit profile", parameters, options);
            //NavigationManager.NavigateTo("/patients/edit");
        }
    }
}
