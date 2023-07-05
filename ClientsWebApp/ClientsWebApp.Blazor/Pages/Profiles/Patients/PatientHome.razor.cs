using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Infrastructure;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Services.Services;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Patients
{
    public partial class PatientHome: CancellableComponent
    {
        [Inject] AuthenticationStateHelper StateHelper { get; set; }
        [Inject] IPatientService PatientService { get; set; }
        [Inject] IAppointmentService AppointmentService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private Patient? Patient { get; set; }
        private IEnumerable<Appointment> Appointments { get; set; } = new List<Appointment>();
        private bool IsLoading { get; set; } = true;
        protected override async void OnInitialized()
        {
            IsLoading = true;
            var email = await StateHelper.GetEmailAsync();
            try
            {
                Patient = await PatientService.GetByEmailAsync(email, _cts.Token);
            }
            catch
            {
                NavigationManager.NavigateTo("/patients/create");
            }

            Appointments = await AppointmentService.GetPageAsync(new Page(5, 1), new AppointmentFiltrationModel(){PatientId = Patient.Id}, _cts.Token);
            IsLoading = false;
            this.StateHasChanged();
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
