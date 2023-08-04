using ClientsWebApp.Application.Abstraction;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    public partial class ApproveAppointment
    {
        [Inject] public IAppointmentManager AppointmentManager { get; set; }
        [Parameter] public Guid AppointmentId { get; set; }
        [Parameter] public EventCallback OnAppointmentApproved { get; set; }

        private bool IsLoading { get; set; } = false;

        private async Task ChangeAsync()
        {
            IsLoading = true;

            await AppointmentManager.ApproveAsync(AppointmentId, _cts.Token);
            await OnAppointmentApproved.InvokeAsync();

            IsLoading = false;
        }
    }
}