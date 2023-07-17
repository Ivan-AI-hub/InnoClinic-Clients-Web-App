using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using Blazored.Modal.Services;
using Blazored.Modal;
using ClientsWebApp.Blazor.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Patients
{
    [Authorize(Roles = "Patient")]
    public partial class PatientHome : CancellableComponent
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Inject] AuthenticationStateHelper StateHelper { get; set; }
        [Inject] IPatientManager PatientManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private PatientDTO? Patient { get; set; }
        private bool IsLoading { get; set; } = true;
        protected override async void OnInitialized()
        {
            IsLoading = true;
            var email = await StateHelper.GetEmailAsync();
            try
            {
                Patient = await PatientManager.GetByEmailAsync(email, _cts.Token);
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
            Modal.Show<DeletePatient>("Delete profile", options);
            //NavigationManager.NavigateTo("/patients/delete");
        }

        private void NavigateToCreatePage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large,
                DisableBackgroundCancel = true
            };
            Modal.Show<CreatePatient>("Create profile", options);
            //NavigationManager.NavigateTo("/patients/create");
        }
        private void NavigateToEditPage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large,
                DisableBackgroundCancel = true
            };
            Modal.Show<EditPatient>("Edit profile", options);
            //NavigationManager.NavigateTo("/patients/edit");
        }
    }
}
