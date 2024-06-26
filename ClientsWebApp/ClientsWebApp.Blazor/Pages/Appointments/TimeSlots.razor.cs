﻿using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Appointments;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Categories;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    public partial class TimeSlots : CancellableComponent
    {
        [Parameter] public Guid DoctorId { get; set; }
        [Parameter] public Guid ExcludeAppointmentId { get; set; } = default;
        [Parameter] public Category Category { get; set; }
        [Parameter] public EventCallback<TimeSlotsData> OnTimeSlotSelected { get; set; }
        [Inject] public IAppointmentManager AppointmentManager { get; set; }
        [Inject] public IServiceManager ServiceManager { get; set; }

        private bool isTableReloading { get; set; } = false;
        private TimeSlotsData Data { get; set; } = new TimeSlotsData();
        private Page Page { get; set; } = new Page(100, 1);
        private AppointmentFiltrationModel FiltrationModel { get; set; } = new AppointmentFiltrationModel();
        private IDictionary<TimeOnly, bool> Times { get; set; } = new Dictionary<TimeOnly, bool>();
        protected async override Task OnInitializedAsync()
        {
            FiltrationModel.DoctorId = DoctorId;

            for (int i = 1; i < 24; i++)
            {
                for (int j = 0; j < 60; j += 10)
                {
                    Times.Add(new TimeOnly(i, j), true);
                }
            }
            await UpdateTimesAsync();
        }

        private async Task UpdateTimesAsync()
        {
            isTableReloading = true;
            foreach (var time in Times)
            {
                Times[time.Key] = true;
            }
            FiltrationModel.Date = Data.Date;
            var appointments = await AppointmentManager.GetPageAsync(Page, FiltrationModel, _cts.Token);
            appointments = appointments.Where(x => x.Id != ExcludeAppointmentId);
            foreach (var appointment in appointments)
            {
                var service = await ServiceManager.GetByIdAsync(appointment.Service.Id, _cts.Token);

                int timeSlots = service.Category.TimeSlotSize / 10;

                var time = appointment.Time;
                Times[time] = false;
                for (int i = 1; i < timeSlots; i++)
                {
                    Times[time.AddMinutes(10)] = false;
                    time = time.AddMinutes(10);
                }
            }
            isTableReloading = false;
            StateHasChanged();
        }

        private async Task SelectTimeSlot(TimeOnly time)
        {
            Data.StartTime = time;
            Data.EndTime = time.AddMinutes(Category.TimeSlotSize - 1 * 10);
            for (TimeOnly i = Data.StartTime; i <= Data.EndTime; i = i.AddMinutes(10))
            {
                if (!Times[i])
                {
                    Data.StartTime = default;
                    Data.EndTime = default;
                    return;
                }
            }
            StateHasChanged();
            await OnTimeSlotSelected.InvokeAsync(Data);
        }

        private async Task DateWasSelected(DateOnly? date)
        {
            Data.Date = date;
            StateHasChanged();

            await UpdateTimesAsync();
        }
    }
}
