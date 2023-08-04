using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Profiles.Patient;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Patients
{
    public partial class PatientsList : CancellableComponent
    {
        [Inject] public IPatientManager PatientManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private Page Page { get; set; }
        private PageStatus Status => Page.GetPageStatus(Patients == null ? 0 : Patients.Count());
        private PatientFiltrationModel FiltrationModel { get; set; }
        private IEnumerable<PatientDTO>? Patients { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            FiltrationModel = new PatientFiltrationModel();
            await PatientsUpdateAsync();
            StateHasChanged();
        }

        private async Task PatientsUpdateAsync()
        {
            SubmitButton?.StartLoading();

            Patients = null;

            Patients = await PatientManager.GetPageAsync(Page, FiltrationModel, _cts.Token);
            StateHasChanged();

            SubmitButton?.StopLoading();
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
        protected PageStatus GetPageStatus()
        {
            return Page.GetPageStatus(Patients == null ? 0 : Patients.Count());
        }

    }
}
