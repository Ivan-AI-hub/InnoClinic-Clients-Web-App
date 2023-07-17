using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Appointments;
using ClientsWebApp.Blazor;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
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
            Specializations = await SpecializationManager.GetInfoAsync(Page, _cts.Token);

            Data.Category = Categories.First();
            Data.PatientId = Patient.Id;
            Data.Specialization = Specializations.First().Name;
            await UpdateDoctorsAsync();
            if (InitialDoctorId != default)
            {
                await DoctorWasSelected(InitialDoctorId);
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

            Data.ServiceId = Services.FirstOrDefault()?.Id ?? default;
            StateHasChanged();
        }

        private void StartSelectDateAndTime()
        {
            IsDateTimeSelection = true;
            StateHasChanged();
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

        private async Task DoctorWasSelected(ChangeEventArgs args)
        {
            var doctorId = Guid.Parse(args.Value.ToString());

            await DoctorWasSelected(doctorId);
        }

        private async Task OfficeWasSelected(ChangeEventArgs args)
        {
            var officeId = Guid.Parse(args.Value.ToString());
            Data.OfficeId = officeId;
            StateHasChanged();

            await UpdateDoctorsAsync();
        }

        private async Task SpecializationWasSelected(ChangeEventArgs args)
        {
            Data.Specialization = args.Value.ToString();
            StateHasChanged();
            await UpdateDoctorsAsync();
        }

        private async Task CategoryWasSelected(ChangeEventArgs args)
        {
            Data.Category = args.Value.ToString();
            await UpdateServicesAsync();
            StateHasChanged();
        }

        private void TimeSlotSelected(TimeSlotsData data)
        {
            Data.Date = data.Date;
            Data.Time = data.StartTime;
            StateHasChanged();
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

            NavigationManager.NavigateTo($"/patients/{Data.PatientId}/appointmentsHistory");
        }
    }
}
