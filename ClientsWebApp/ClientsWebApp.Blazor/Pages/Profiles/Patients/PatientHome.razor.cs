using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
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
        [Inject] private IAppointmentManager _appointmentManager { get; set; }
        private IEnumerable<Appointment>? Appointments { get; set; }
        private Page Page { get; set; } = new Page(20, 1);

        private PatientDTO? Patient { get; set; }
        protected override async void OnInitialized()
        {
            var email = await StateHelper.GetEmailAsync();
            try
            {
                Patient = await PatientManager.GetByEmailAsync(email, _cts.Token);
            }
            catch (NotFoundException)
            {
                NavigateToCreatePage();
            }

            var filtrationModel = new AppointmentFiltrationModel
            {
                PatientId = Patient.Id,
                Status = null
            };
            Appointments = await _appointmentManager.GetPageAsync(Page, filtrationModel, _cts.Token);

            StateHasChanged();
        }

        private void NavigateToDeletePage()
        {
            Modal.Show<DeletePatient>("Delete profile");
            //NavigationManager.NavigateTo("/patients/delete");
        }

        private void NavigateToCreatePage()
        {
            var options = new ModalOptions() 
            {
                Size = ModalSize.Large,
                DisableBackgroundCancel = true,
            };
            Modal.Show<CreatePatient>("Create profile", options);
            //NavigationManager.NavigateTo("/patients/create");
        }
        private void NavigateToEditPage()
        {
            var options = new ModalOptions() 
            {
                Size = ModalSize.Large
            };
            var parameters = new ModalParameters();
            parameters.Add(nameof(EditPatient.OldPatient), Patient);
            Modal.Show<EditPatient>("Edit profile", parameters, options);
            //NavigationManager.NavigateTo("/patients/edit");
        }
        private void NavigateToHistoryPage()
        {
            NavigationManager.NavigateTo($"/patients/{Patient.Id}/appointmentsHistory");
        }
    }
}
