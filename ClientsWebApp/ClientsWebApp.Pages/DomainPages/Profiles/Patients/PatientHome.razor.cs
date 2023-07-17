using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Profiles.Patients
{
    [Authorize(Roles = "Patient")]
    public partial class PatientHome : CancellableComponent
    {
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
                NavigationManager.NavigateTo("/patients/create");
            }

            IsLoading = false;
            StateHasChanged();
        }

        private void NavigateToDeletePage()
        {
            NavigationManager.NavigateTo("/patients/delete");
        }

        private void NavigateToEditPage()
        {
            NavigationManager.NavigateTo("/patients/edit");
        }
    }
}
