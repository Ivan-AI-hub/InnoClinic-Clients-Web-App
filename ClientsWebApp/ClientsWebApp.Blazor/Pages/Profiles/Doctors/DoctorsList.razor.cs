using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Services.Services;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Doctors
{
    public partial class DoctorsList : CancellableComponent
    {
        [Inject] public IDoctorService DoctorService { get; set; }

        private Page Page { get; set; }
        private PageStatus Status => Page.GetPageStatus(Doctors == null ? 0 : Doctors.Count());
        private DoctorFiltrationModel FiltrationModel { get; set; }

        private IEnumerable<Doctor>? Doctors { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            FiltrationModel = new DoctorFiltrationModel();
            await DoctorsUpdateAsync();
        }

        private async Task DoctorsUpdateAsync()
        {
            Doctors = await DoctorService.GetPageAsync(Page, FiltrationModel, _cts.Token);
            this.StateHasChanged();
        }

        protected async Task SetPreviousPage()
        {
            Page.Number--;
            await DoctorsUpdateAsync();
        }
        protected async Task SetNextPage()
        {
            Page.Number++;
            await DoctorsUpdateAsync();
        }


    }
}
