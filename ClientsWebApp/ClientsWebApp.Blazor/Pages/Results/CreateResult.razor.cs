using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Managers;
using ClientsWebApp.Application.Models.Results;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Appointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Results
{

    [Authorize(Roles = "Doctor")]
    public partial class CreateResult : CancellableComponent
    {
        [Parameter] public Guid AppointmentId { get; set; }
        [Inject] public IAppointmentManager AppointmentManager { get; set; }
        [Inject] public IResultManager ResultManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private CreateResultData Data { get; set; } = new CreateResultData();
        private Appointment Appointment { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Appointment = await AppointmentManager.GetByIdAsync(AppointmentId, _cts.Token);
            Data = new CreateResultData() { AppointmentId = Appointment.Id, DoctorId = Appointment.Doctor.Id, PatientId = Appointment.Patient.Id };
            StateHasChanged();
        }
        private async Task CreateAsync()
        {
            SubmitButton?.StartLoading();
            try
            {
                await ResultManager.CreateAsync(Data, _cts.Token);
            }
            finally
            {
                SubmitButton?.StopLoading();
            }
            Cancel();
        }
        private void Cancel()
        {
            NavigationManager.NavigateTo($"/appointments/{AppointmentId}");
        }
    }
}