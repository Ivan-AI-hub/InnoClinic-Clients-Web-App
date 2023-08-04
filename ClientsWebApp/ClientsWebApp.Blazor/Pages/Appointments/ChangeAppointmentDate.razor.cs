using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Application.Models.Appointments;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Categories;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    public partial class ChangeAppointmentDate
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Parameter] public Appointment Appointment { get; set; }
        [Parameter] public Category Category { get; set; }
        [Parameter] public EventCallback OnDateChanged { get; set; }

        private IModalReference? _openedModal;

        private bool IsLoading { get; set; } = false;
        private ChangeDateData Data { get; set; }

        protected override void OnInitialized()
        {
            Data = new ChangeDateData()
            {
                AppointmentId = Appointment.Id,
                DoctorId = Appointment.Doctor.Id
            };
        }

        private void StartChanging()
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(TimeSlots.ExcludeAppointmentId), Appointment.Id);
            parameters.Add(nameof(TimeSlots.Category), Category);
            parameters.Add(nameof(TimeSlots.DoctorId), Appointment.Doctor.Id);
            parameters.Add(nameof(TimeSlots.OnTimeSlotSelected), EventCallback.Factory.Create<TimeSlotsData>(this, ChangeAsync));
            _openedModal = Modal.Show<TimeSlots>("Select date", parameters);
        }

        private async Task ChangeAsync(TimeSlotsData timeSlotsData)
        {
            IsLoading = true;
            Data.Date = timeSlotsData.Date.ToDateTime(timeSlotsData.StartTime);
            await AppointmentManager.ChangeDateAsync(Data, _cts.Token);
            await OnDateChanged.InvokeAsync();
            _openedModal?.Close();

            IsLoading = false;
        }
    }
}