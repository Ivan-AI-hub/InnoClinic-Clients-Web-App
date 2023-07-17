using ClientsWebApp.Application.Models.Appointments;
using ClientsWebApp.Domain.Appointments;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    public partial class ChangeAppointmentDate
    {
        [Parameter]
        public Appointment Appointment { get; set; }

        [Parameter]
        public EventCallback OnDateChanged { get; set; }

        private bool IsDateChanges { get; set; }

        private bool IsLoading { get; set; } = false;
        private ChangeDateData Data { get; set; }

        protected override void OnInitialized()
        {
            Data = new ChangeDateData()
            {
                Date = Appointment.Date.ToDateTime(Appointment.Time),
                AppointmentId = Appointment.Id,
                DoctorId = Appointment.Doctor.Id
            };
        }

        private void StartChanging()
        {
            IsDateChanges = true;
        }

        private void StopChanging()
        {
            IsDateChanges = false;
        }

        private async Task ChangeAsync()
        {
            IsLoading = true;
            await AppointmentManager.ChangeDateAsync(Data, _cts.Token);
            await OnDateChanged.InvokeAsync();
            StopChanging();
            IsLoading = false;
        }
    }
}