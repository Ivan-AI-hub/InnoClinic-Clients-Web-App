using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Categories;
using ClientsWebApp.Domain.Services;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.Pages.Appointments
{
    public partial class TimeSlots : CancellableComponent
    {
        [Parameter] public Guid DoctorId { get; set; }
        [Parameter] public Category Category { get; set; }
        [Parameter] public EventCallback<TimeSlotsData> OnTimeSlotSelected { get; set; }
        [Inject] public IAppointmentService AppointmentService { get; set; }
        [Inject] public IServiceService ServiceService { get; set; }
        [Inject] public ILogger<TimeSlots> Logger { get; set; }

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
            var appointments = await AppointmentService.GetPageAsync(Page, FiltrationModel, _cts.Token);

            foreach (var appointment in appointments)
            {
                var service = await ServiceService.GetByIdAsync(appointment.Service.Id, _cts.Token);

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
            StateHasChanged();
            await OnTimeSlotSelected.InvokeAsync(Data);
        }

        private async Task DateWasSelected(ChangeEventArgs args)
        {
            var date = DateOnly.Parse(args.Value.ToString());
            Data.Date = date;
            StateHasChanged();

            await UpdateTimesAsync();
        }
    }
}
