using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    public partial class CancelAppointment
    {
        [Parameter] public Guid AppointmentId { get; set; }

        [Parameter] public EventCallback OnAppointmentCanceled { get; set; }

        private bool IsLoading { get; set; } = false;
        private async Task CancelAsync()
        {
            IsLoading = true;
            await AppointmentManager.CancelAsync(AppointmentId, _cts.Token);
            await OnAppointmentCanceled.InvokeAsync();
            IsLoading = false;
        }
    }
}