using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Profiles.Patient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ClientsWebApp.Blazor.Pages.Profiles.Patients
{
    public partial class PatientInfo
    {
        [Parameter] public Guid PatientId { get; set; }
        [Inject] private IAppointmentManager _appointmentManager { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private IEnumerable<Appointment>? Appointments { get; set; }
        private Page Page { get; set; } = new Page(20, 1);
        private PatientDTO patient;
        protected async override Task OnInitializedAsync()
        {
            patient = await PatientManager.GetByIdAsync(PatientId, _cts.Token);

            var filtrationModel = new AppointmentFiltrationModel
            {
                PatientId = PatientId,
                Status = null
            };

            Appointments = await _appointmentManager.GetPageAsync(Page, filtrationModel, _cts.Token);
        }

        private void NavigateToHistoryPage()
        {
            NavigationManager.NavigateTo($"/patients/{PatientId}/appointmentsHistory");
        }
    }
}