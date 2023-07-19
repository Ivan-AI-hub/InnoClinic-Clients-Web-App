using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Exceptions;
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
            catch (NotFoundException ex)
            {
                NavigateToCreatePage();
            }

            IsLoading = false;
            StateHasChanged();
        }

        private void NavigateToDeletePage()
        {
            Modal.Show<DeletePatient>("Delete profile");
            //NavigationManager.NavigateTo("/patients/delete");
        }

        private void NavigateToCreatePage()
        {
            Modal.Show<CreatePatient>("Create profile");
            //NavigationManager.NavigateTo("/patients/create");
        }
        private void NavigateToEditPage()
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(EditPatient.OldPatient), Patient);
            Modal.Show<EditPatient>("Edit profile", parameters);
            //NavigationManager.NavigateTo("/patients/edit");
        }
    }
}
