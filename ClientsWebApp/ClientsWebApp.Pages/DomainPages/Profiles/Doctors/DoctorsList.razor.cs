using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain.Specializations;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Profiles.Doctors
{
    public partial class DoctorsList : CancellableComponent
    {
        [Inject] public IDoctorManager DoctorManager { get; set; }
        [Inject] public ISpecializationManager SpecializationManager { get; set; }
        [Inject] public IOfficeManager OfficeManager { get; set; }

        private Page Page { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private DoctorFiltrationModel FiltrationModel { get; set; }
        private IEnumerable<DoctorDTO>? Doctors { get; set; }
        private IEnumerable<Specialization>? Specializations { get; set; }
        private IEnumerable<Office>? Offices { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            FiltrationModel = new DoctorFiltrationModel();
            await DoctorsUpdateAsync();
            Offices = await OfficeManager.GetPageAsync(new Page(100, 1), _cts.Token);
            Specializations = await SpecializationManager.GetInfoAsync(new Page(100, 1), _cts.Token);
        }

        private async Task DoctorsUpdateAsync()
        {
            SubmitButton?.StartLoading();

            Doctors = null;
            StateHasChanged();

            Doctors = await DoctorManager.GetPageAsync(Page, FiltrationModel, _cts.Token);
            StateHasChanged();

            SubmitButton?.StopLoading();
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
        protected PageStatus GetPageStatus()
        {
            return Page.GetPageStatus(Doctors == null ? 0 : Doctors.Count());
        }

    }
}
