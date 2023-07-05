using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Domain.Profiles.Patient;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Patients
{
    public partial class PatientsList : CancellableComponent
    {
        [Inject] public IPatientService PatientService { get; set; }

        private Page Page { get; set; }
        private PageStatus Status => Page.GetPageStatus(Patients == null ? 0 : Patients.Count());
        private PatientFiltrationModel FiltrationModel { get; set; }
        private IEnumerable<Patient>? Patients { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            FiltrationModel = new PatientFiltrationModel();
            await PatientsUpdateAsync();
            this.StateHasChanged();
        }

        private async Task PatientsUpdateAsync()
        {
            Patients = await PatientService.GetPageAsync(Page, FiltrationModel, _cts.Token);
            this.StateHasChanged();
        }

        protected async Task SetPreviousPage()
        {
            Page.Number--;
            await PatientsUpdateAsync();
        }
        protected async Task SetNextPage()
        {
            Page.Number++;
            await PatientsUpdateAsync();
        }

    }
}
