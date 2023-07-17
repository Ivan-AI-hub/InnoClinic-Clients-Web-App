using ClientsWebApp.Application.Abstraction;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Appointments
{
    public partial class ApproveAppointment
    {
        [Inject] public IAppointmentManager AppointmentManager { get; set; }
        [Parameter] public Guid AppointmentId { get; set; }
        [Parameter] public EventCallback OnAppointmentApproved { get; set; }

        private bool IsDateChanges { get; set; }

        private bool IsLoading { get; set; } = false;
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
            await AppointmentManager.ApproveAsync(AppointmentId, _cts.Token);
            await OnAppointmentApproved.InvokeAsync();
            StopChanging();
            IsLoading = false;
        }
    }
}