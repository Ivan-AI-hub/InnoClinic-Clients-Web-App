using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Results;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Exceptions;
using ClientsWebApp.Domain.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClientsWebApp.Blazor.Pages.Results
{
    [Authorize(Roles = "Doctor")]
    public partial class ManageAppointmentResult : CancellableComponent
    {
        [Parameter] public Guid AppointmentId { get; set; }
        [Inject] public IAppointmentManager AppointmentManager { get; set; } = default!;
        [Inject] public IResultManager ResultManager { get; set; } = default!;
        private AppointmentResult? AppointmentResult { get; set; }
        private FormSubmitButton SubmitButton { get; set; } = default!;
        private CreateResultData CreateData { get; set; } = new CreateResultData();
        private EditResultData? EditData { get; set; }

        private Appointment? Appointment { get; set; }
        protected override async Task OnInitializedAsync()
        {
            try
            {
                AppointmentResult ??= await ResultManager.GetForAppointmentAsync(AppointmentId, _cts.Token);
                EditData = new EditResultData(AppointmentResult);
                Appointment = await AppointmentManager.GetByIdAsync(AppointmentId, _cts.Token);
            }
            catch (NotFoundException)
            {
                Appointment = await AppointmentManager.GetByIdAsync(AppointmentId, _cts.Token);
                CreateData = new CreateResultData() { AppointmentId = Appointment.Id, DoctorId = Appointment.Doctor.Id, PatientId = Appointment.Patient.Id };
            }

            StateHasChanged();
        }
        private async Task CreateAsync()
        {
            if (AppointmentResult is not null)
            {
                return;
            }

            SubmitButton?.StartLoading();
            try
            {
                await ResultManager.CreateAsync(CreateData, _cts.Token);
                AppointmentResult ??= await ResultManager.GetForAppointmentAsync(AppointmentId, _cts.Token);
                EditData = new EditResultData(AppointmentResult);
            }
            finally
            {
                SubmitButton?.StopLoading();
            }
        }

        private async Task EditAsync()
        {
            if(AppointmentResult is null)
            {
                return;
            }

            SubmitButton?.StartLoading();
            try
            {
                await ResultManager.EditAsync(AppointmentResult.Id, EditData, _cts.Token);
            }
            finally
            {
                SubmitButton?.StopLoading();
            }
        }
    }
}