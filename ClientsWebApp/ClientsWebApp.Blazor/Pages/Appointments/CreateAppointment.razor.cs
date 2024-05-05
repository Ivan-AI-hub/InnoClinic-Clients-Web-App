using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Appointments;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Categories;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Domain.Services;
using ClientsWebApp.Domain.Specializations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    [Authorize(Roles = "Patient")]
    public partial class CreateAppointment : CancellableComponent
    {
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IAppointmentManager AppointmentManager { get; set; }
        [Inject] public ISpecializationManager SpecializationManager { get; set; }
        [Inject] public IPatientManager PatientManager { get; set; }
        [Inject] public IDoctorManager DoctorManager { get; set; }
        [Inject] public IServiceManager ServiceManager { get; set; }
        [Inject] public IOfficeManager OfficeManager { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "DoctorId")]
        public Guid InitialDoctorId { get; set; } = default;

        [Parameter]
        [SupplyParameterFromQuery(Name = "ServiceId")]
        public Guid InitialServiceId { get; set; } = default;

        [Parameter]
        [SupplyParameterFromQuery(Name = "Specialization")]
        public string InitialSpecialization { get; set; } = default;
        [Parameter]
        [SupplyParameterFromQuery(Name = "Category")]
        public string InitialCategory { get; set; } = default;

        private Patient? Patient { get; set; }
        private bool IsDateTimeSelection { get; set; }
        private IEnumerable<Specialization> Specializations { get; set; } = new List<Specialization>();
        private IEnumerable<Doctor> Doctors { get; set; } = new List<Doctor>();
        private IEnumerable<Service> Services { get; set; } = new List<Service>();
        private IEnumerable<Office> Offices { get; set; } = new List<Office>();
        private IEnumerable<string> Categories = new List<string>() { "consultation", "analyses", "diagnostics" };
        private CreateAppointmentData Data { get; set; } = new CreateAppointmentData();
        protected FormSubmitButton SubmitButton { get; set; }
        private Page Page { get; set; } = new Page(100, 1);
        private DoctorFiltrationModel FiltrationModel { get; set; } = new DoctorFiltrationModel();


        private string ErrorMessage { get; set; } = "";

        protected async override Task OnInitializedAsync()
        {
            var email = await authStateHelper.GetEmailAsync();

            Patient = await PatientManager.GetInfoByEmailAsync(email, _cts.Token);
            Offices = await OfficeManager.GetInfoPageAsync(Page, _cts.Token);
            Offices = Offices.Where(x => x.Status);
            Specializations = await SpecializationManager.GetInfoAsync(Page, _cts.Token);
            Specializations = Specializations.Where(x => x.IsActive);

            Data.PatientId = Patient.Id;

            if (InitialCategory != default && Categories.Any(x => x == InitialCategory))
            {
                Data.Category = InitialCategory;

            }
            else
            {
                Data.Category = Categories.First();
            }

            if (InitialSpecialization != default && Specializations.Any(x => x.Name == InitialSpecialization))
            {
                Data.Specialization = InitialSpecialization;
            }
            else
            {
                Data.Specialization = Specializations.FirstOrDefault()?.Name ?? "";
            }

            await UpdateDoctorsAsync();
            if (InitialDoctorId != default)
            {
                await DoctorWasSelected(InitialDoctorId);
            }
            if (InitialServiceId != default)
            {
                Data.ServiceId = InitialServiceId;
            }
        }

        protected async Task UpdateDoctorsAsync()
        {
            FiltrationModel.OfficeId = Data.OfficeId;
            FiltrationModel.Specialization = Data.Specialization;

            Doctors = await DoctorManager.GetInfoPageAsync(Page, FiltrationModel, _cts.Token);
            Data.DoctorId = Doctors.FirstOrDefault()?.Id ?? default;
            StateHasChanged();
            await DoctorWasSelected(Data.DoctorId);
        }
        protected async Task UpdateServicesAsync()
        {
            var specialization = Specializations.FirstOrDefault(x => x.Name == Data.Specialization);
            Services = await ServiceManager.GetBySpecializationAsync(Data.Specialization, _cts.Token);

            Services = Services.Where(x => x.Category.Name == Data.Category);
            Data.ServiceId = Services.FirstOrDefault()?.Id ?? default;
            StateHasChanged();
        }

        private void StartSelectDateAndTime()
        {
            if(Data.ServiceId != default && Data.DoctorId != default)
            {
                IsDateTimeSelection = true;
                StateHasChanged();
            }
        }
        private void StopSelectDateAndTime()
        {
            IsDateTimeSelection = false;
            StateHasChanged();
        }
        private async Task DoctorWasSelected(Guid doctorId)
        {
            var doctor = Doctors.FirstOrDefault(x => x.Id == doctorId);

            if (doctor != null)
            {
                Data.DoctorId = doctor.Id;
                Data.Specialization = doctor.Specialization;
                Data.OfficeId = doctor.Office.Id;
                await UpdateServicesAsync();
            }
            StateHasChanged();
        }

        private async Task DoctorWasSelected()
        {
            StateHasChanged();

            await DoctorWasSelected(Data.DoctorId);
        }

        private async Task OfficeWasSelected()
        {
            StateHasChanged();

            await UpdateDoctorsAsync();
        }

        private async Task SpecializationWasSelected()
        {
            StateHasChanged();

            await UpdateDoctorsAsync();
        }

        private async Task CategoryWasSelected()
        {
            StateHasChanged();

            await UpdateServicesAsync();
        }

        private void TimeSlotSelected(TimeSlotsData data)
        {
            Data.Date = data.Date ?? DateOnly.MinValue;
            Data.Time = data.StartTime;
            StateHasChanged();
        }

        private Category GetCategory(string name)
        {
            int timeslot = 10;
            switch (name)
            {
                case "consultation":
                    timeslot = 10;
                    break;
                case "analyses":
                    timeslot = 20;
                    break;
                case "diagnostics":
                    timeslot = 30;
                    break;
                default: throw new NotImplementedException();
            }
            return new Category(default, Data.Category, timeslot, Services.ToList());
        }
        private async Task CreateAsync()
        {
            SubmitButton.StartLoading();
            try
            {
                await AppointmentManager.CreateAsync(Data, _cts.Token);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            finally
            {
                SubmitButton.StopLoading();
            }

            Cancel();
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo($"/patients/{Data.PatientId}/appointmentsHistory");
        }
    }
}
