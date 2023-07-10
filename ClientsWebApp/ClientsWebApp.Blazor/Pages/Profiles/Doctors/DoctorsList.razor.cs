using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain.Specializations;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Doctors
{
    public partial class DoctorsList : CancellableComponent
    {
        [Inject] public IDoctorService DoctorService { get; set; }
        [Inject] public ISpecializationService SpecializationService { get; set; }
        [Inject] public IOfficeService OfficeService { get; set; }

        private Page Page { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private DoctorFiltrationModel FiltrationModel { get; set; }
        private IEnumerable<Doctor>? Doctors { get; set; }
        private IEnumerable<Specialization>? Specializations { get; set; }
        private IEnumerable<Office>? Offices { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            FiltrationModel = new DoctorFiltrationModel();
            await DoctorsUpdateAsync();
            Offices = await OfficeService.GetPageAsync(new Page(100, 1), _cts.Token);
            Specializations = await SpecializationService.GetInfoAsync(new Page(100, 1), _cts.Token);
        }

        private async Task DoctorsUpdateAsync()
        {
            SubmitButton?.StartLoading();

            Doctors = null;
            this.StateHasChanged();

            Doctors = await DoctorService.GetPageAsync(Page, FiltrationModel, _cts.Token);
            this.StateHasChanged();

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
