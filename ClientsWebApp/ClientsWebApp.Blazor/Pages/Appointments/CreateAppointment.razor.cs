using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Infrastructure;
using ClientsWebApp.Blazor.Pages.Appointments.Models;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Domain.Services;
using ClientsWebApp.Domain.Specializations;
using ClientsWebApp.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    [Authorize(Roles = "Patient")]
    public partial class CreateAppointment : CancellableComponent
    {
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IAppointmentService AppointmentService { get; set; }
        [Inject] public ISpecializationService SpecializationService { get; set; }
        [Inject] public IPatientService PatientService { get; set; }
        [Inject] public IDoctorService DoctorService { get; set; }
        [Inject] public IServiceService ServiceService { get; set; }
        [Inject] public IOfficeService OfficeService { get; set; }
        [Inject] public ILogger<CreateAppointment> Logger { get; set; }

        private Patient? Patient { get; set; }
        private TimeSlots TimeSlots { get; set; }
        private IEnumerable<Specialization>? Specializations { get; set; }
        private IEnumerable<Doctor>? Doctors { get; set; }
        private IEnumerable<Service>? Services { get; set; }
        private IEnumerable<Office>? Offices { get; set; }
        private CreateAppointmentData Data { get; set; }
        private Page Page { get; set; }
        private DoctorFiltrationModel FiltrationModel { get; set; }

        private List<string> Categories = new List<string>() { "consultation", "analyses", "diagnostics" };
        private string ErrorMessage { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Page = new Page(100, 1);
            Data = new CreateAppointmentData();

            FiltrationModel = new DoctorFiltrationModel();
            var email = await authStateHelper.GetEmailAsync();

            Patient = await PatientService.GetByEmailAsync(email, _cts.Token);
            Offices = await OfficeService.GetPageAsync(Page, _cts.Token);
            Specializations = await SpecializationService.GetInfoAsync(Page, _cts.Token);

            Data.Category = Categories.First();
            Data.PatientId = Patient.Id;
            Data.Specialization = Specializations.First().Name;
            await UpdateDoctorsAsync();
        }

        protected async Task UpdateDoctorsAsync()
        {
            FiltrationModel.OfficeId = Data.OfficeId;
            FiltrationModel.Specialization = Data.Specialization;
            
            Doctors = await DoctorService.GetPageAsync(Page, FiltrationModel, _cts.Token);
            Data.DoctorId = Doctors.FirstOrDefault()?.Id ?? default;
            await DoctorWasSelected(Data.DoctorId);
            this.StateHasChanged();
        }
        protected async Task UpdateServicesAsync()
        {
            var specialization = Specializations.First(x => x.Name == Data.Specialization);
            Services = await ServiceService.GetByCategoryAsync(Data.Category, Page, _cts.Token);
            Services = Services.Where(x => x.SpecializationId == specialization.Id);

            Data.ServiceId = Services.FirstOrDefault()?.Id ?? default;
            this.StateHasChanged();
        }

        private async Task DoctorWasSelected(ChangeEventArgs args)
        {
            var doctorId = Guid.Parse(args.Value.ToString()); 

            await DoctorWasSelected(doctorId);
        }
        private async Task DoctorWasSelected(Guid doctorId)
        {
            var doctor = Doctors.FirstOrDefault(x => x.Id == doctorId);

            if (doctor != null)
            {
                Data.DoctorId = doctor.Id;
                Data.Specialization = doctor.Specialization;
                Data.OfficeId = doctor.Office.Id;
                if(TimeSlots != null)
                {
                    await TimeSlots.UpdateTimesAsync(doctor.Id);
                }
                await UpdateServicesAsync();
            }
            this.StateHasChanged();
        }

        private async Task OfficeWasSelected(ChangeEventArgs args)
        {
            var officeId = Guid.Parse(args.Value.ToString());
            Data.OfficeId = officeId;
            await UpdateDoctorsAsync();
            this.StateHasChanged();
        }

        private async Task SpecializationWasSelected(ChangeEventArgs args)
        {
            Data.Specialization = args.Value.ToString();
            await UpdateDoctorsAsync();
            this.StateHasChanged();
        }

        private async Task CategoryWasSelected(ChangeEventArgs args)
        {
            Data.Category = args.Value.ToString();
            await UpdateServicesAsync();
            this.StateHasChanged();
        }

        private void TimeSlotSelected(TimeSlotsData data)
        {
            Data.Date = data.Date;
            Data.Time = data.Time;
            this.StateHasChanged();
        }
        private async Task CreateAsync()
        {

            try
            {
                var appointment = await AppointmentService.CreateAsync(new CreateAppointmentModel(Data.PatientId, Data.DoctorId, Data.ServiceId, Data.Date, Data.Time), _cts.Token);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
        }
    }
}
