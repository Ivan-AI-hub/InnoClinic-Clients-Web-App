using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Offices;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    [Authorize(Roles ="Admin")]
    public partial class AppointmentsList : CancellableComponent
    {
        [Inject] public IAppointmentService AppointmentService { get; set; }
        [Inject] public IOfficeService OfficeService { get; set; }

        private Page Page { get; set; }
        private PageStatus Status => Page.GetPageStatus(Appointments == null ? 0 : Appointments.Count());
        private AppointmentFiltrationModel FiltrationModel { get; set; }

        private IEnumerable<Appointment>? Appointments { get; set; }
        private IEnumerable<Office>? Offices { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            FiltrationModel = new AppointmentFiltrationModel();
            Offices = await OfficeService.GetPageAsync(new Page(100, 1), _cts.Token);
            await AppointmentsUpdateAsync();
        }

        private async Task AppointmentsUpdateAsync()
        {
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
