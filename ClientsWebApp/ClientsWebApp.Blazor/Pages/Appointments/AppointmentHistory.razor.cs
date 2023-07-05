using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    [Authorize(Roles="Doctor,Patient")]
    public partial class AppointmentHistory : CancellableComponent
    {
        [Inject] public IAppointmentService AppointmentService { get; set; }
        [Parameter] public Guid PatientId { get; set; }

        private Page Page { get; set; }
        private PageStatus Status => Page.GetPageStatus(Appointments == null ? 0 : Appointments.Count());
        private AppointmentFiltrationModel FiltrationModel { get; set; }
        private IEnumerable<Appointment>? Appointments { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            FiltrationModel = new AppointmentFiltrationModel
            {
                PatientId = PatientId
            };
            await AppointmentsUpdateAsync();
            this.StateHasChanged();
        }

        private async Task AppointmentsUpdateAsync()
        {
            Appointments = null;
            Appointments = await AppointmentService.GetPageAsync(Page, FiltrationModel, _cts.Token);
            this.StateHasChanged();
        }
        protected async Task SetPreviousPage()
        {
            Page.Number--;
            await AppointmentsUpdateAsync();
        }
        protected async Task SetNextPage()
        {
            Page.Number++;
            await AppointmentsUpdateAsync();
        }

    }
}
