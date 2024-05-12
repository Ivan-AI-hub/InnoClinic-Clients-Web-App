using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    [Authorize(Roles = "Admin")]
    public partial class AppointmentsList : CancellableComponent
    {
        [Inject] public IAppointmentManager AppointmentManager { get; set; }

        private Page Page { get; set; } = new Page(4, 1);
        protected FormSubmitButton SubmitButton { get; set; }
        private AppointmentFiltrationModel FiltrationModel { get; set; }

        private IEnumerable<Appointment>? Appointments { get; set; }
        protected override async Task OnInitializedAsync()
        {
            FiltrationModel = new AppointmentFiltrationModel();
            await AppointmentsUpdateAsync();
        }

        private async Task AppointmentsUpdateAsync()
        {
            SubmitButton?.StartLoading();

            Appointments = null;
            StateHasChanged();
            Appointments = await AppointmentManager.GetPageAsync(Page, FiltrationModel, _cts.Token);
            StateHasChanged();

            SubmitButton?.StopLoading();
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
        protected PageStatus GetPageStatus()
        {
            return Page.GetPageStatus(Appointments == null ? 0 : Appointments.Count());
        }
    }
}
