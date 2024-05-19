using Blazored.Modal.Services;
using Blazored.Modal;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Exceptions;
using ClientsWebApp.Shared.Patient.Models.MedicalRecords;
using ClientsWebApp.Shared.Patient.Models;
using ClientsWebApp.Shared.Patient;
using Microsoft.AspNetCore.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Application.Models.Doctors;

namespace ClientsWebApp.Blazor.Pages.Profiles.Patients
{
    public partial class PatientMedicalCard : CancellableComponent
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Parameter] public Guid PatientId { get; set; }
        [Inject] AuthenticationStateHelper StateHelper { get; set; }
        [Inject] IPatientManager PatientManager { get; set; }
        [Inject] IDoctorManager DoctorManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] private IAppointmentManager _appointmentManager { get; set; }
        private IEnumerable<Appointment>? Appointments { get; set; }
        private Page Page { get; set; } = new Page(100, 1);

        private PatientDTO? Patient { get; set; }
        private PatientPersonalInfo? PatientPersonalInfo { get; set; }

        private Role UserRole { get; set; }

        private bool IsDoctor => UserRole == Role.Doctor;
        private bool IsAdmin => UserRole == Role.Admin;
        private bool IsPatient=> UserRole == Role.Patient;

        private Guid? DoctorId { get; set; }
        private DoctorDTO? Doctor { get; set; }
        protected override async void OnInitialized()
        {
            UserRole = Enum.Parse<Role>( await StateHelper.GetRoleAsync());
            if(IsDoctor)
            {
                var email = await StateHelper.GetEmailAsync();
                Doctor = await DoctorManager.GetByEmailAsync(email, _cts.Token);
                DoctorId = Doctor.Id;
            }
            Console.WriteLine($"Role: {UserRole}");
            try
            {
                Patient = await PatientManager.GetByIdAsync(PatientId, _cts.Token);
            }
            catch (NotFoundException)
            {
                NavigateToHome();
            }

            StateHasChanged();

            try
            {
                PatientPersonalInfo = await PatientManager.GetInfoForPatientAsync(Patient.Id, _cts.Token);
            }
            catch (NotFoundException)
            {
                PatientPersonalInfo = new PatientPersonalInfo(Patient.Id);
                PatientPersonalInfo = await PatientManager.CreateInfoAsync(PatientPersonalInfo, _cts.Token);
            }


            var filtrationModel = new AppointmentFiltrationModel
            {
                PatientId = Patient.Id,
                Status = null
            };
            Appointments = await _appointmentManager.GetPageAsync(Page, filtrationModel, _cts.Token);
            StateHasChanged();

        }

        private void Refresch()
        {
            StateHasChanged();
        }
        private async Task UpdateCardAsync()
        {
            await PatientManager.UpdateInfoAsync(PatientId, PatientPersonalInfo, _cts.Token);
        }
        private void NavigateToHome()
        {
            NavigationManager.NavigateTo("/");
        }
       
        private void NavigateToHistoryPage()
        {
            NavigationManager.NavigateTo($"/patients/{Patient.Id}/appointmentsHistory");
        }


        private void AddRepresenter()
        {
            PatientPersonalInfo.Representer = new HumanInfo("", "", "", "", DateTime.UtcNow);
        }

        private void AddHereditaryIllness()
        {
            if(!IsDoctor)
            {
                return;
            }
            PatientPersonalInfo?.PatientMedicalInfo.FamilyInfo.HereditaryIllnesses.Add(new Illness("", DateTime.UtcNow, DoctorId));
        }

        private void AddPastIllness()
        {
            if (!IsDoctor)
            {
                return;
            }
            PatientPersonalInfo?.PatientMedicalInfo.FamilyInfo.PastIllnesses.Add(new Illness("", DateTime.UtcNow, DoctorId));
        }

        private void AddRiskFactors()
        {
            PatientPersonalInfo?.PatientMedicalInfo.FamilyInfo.RiskFactors.Add(new RiskFactor(""));
        }
        private void AddBodyFeature()
        {
            PatientPersonalInfo?.PatientMedicalInfo.LifeInfo.BodyFeatures.Add(new BodyFeature(""));
        }
        private void AddLifeCondition()
        {
            PatientPersonalInfo?.PatientMedicalInfo.LifeInfo.LifeConditions.Add(new LifeCondition(""));
        }
        private void AddWorkCondition()
        {
            PatientPersonalInfo?.PatientMedicalInfo.LifeInfo.WorkConditions.Add(new WorkCondition(""));
        }
        private void AddAllergicIllness()
        {
            if (!IsDoctor)
            {
                return;
            }
            PatientPersonalInfo?.PatientMedicalInfo.HealthInfo.AllergicIllnesses.Add(new Illness("", DateTime.UtcNow, DoctorId));
        }
        private void AddMedicalReaction()
        {
            PatientPersonalInfo?.PatientMedicalInfo.HealthInfo.MedicalReactions.Add(new MedicalReaction("", DateTime.UtcNow, ""));
        }
        private void AddPregnancy()
        {
            PatientPersonalInfo?.PatientMedicalInfo.WomanHealthInfo.Pregnancies.Add(new WomanPregnancy(DateTime.UtcNow, DateTime.UtcNow, ""));
        }

        private void AddBloodTransfer()
        {
            PatientPersonalInfo?.PatientMedicalInfo.PhysicInfo.BloodTransfers.Add(new BloodTransfer("", 0, DateTime.UtcNow));
        }

        private void AddPreventiveVactination()
        {
            PatientPersonalInfo?.PatientMedicalInfo.PreventiveVaccinations.Add(new PreventiveVaccination(DateTime.UtcNow, ""));
        }

        private void AddIncapacityWorkCertificates()
        {
            PatientPersonalInfo?.PatientMedicalInfo.IncapacityWorkCertificates.Add(new IncapacityWorkCertificate("", "", "", DateTime.UtcNow, DateTime.UtcNow));
        }

        private void AddDisability()
        {
            PatientPersonalInfo?.PatientMedicalInfo.Disabilities.Add(new Disability("", DateTime.UtcNow));
        }

        private void AddRegister()
        {
            PatientPersonalInfo?.PatientMedicalInfo.Registers.Add(new Register(DateTime.UtcNow, "", ""));
        }

        private void AddAggrement()
        {
            PatientPersonalInfo?.PatientMedicalInfo.Aggrements.Add(new Aggrement("", "", ""));
        }

        private void AddRecipeMedicals(bool isPreferencial)
        {
            if (!IsDoctor)
            {
                return;
            }
            PatientPersonalInfo?.PatientMedicalInfo.MedicalSupport.RecipeMedicals.Add(new MedicalRecipeRecord(DoctorId.Value, "", 0, "", new MedicalRecipe("", "", DateTime.UtcNow), 0, isPreferencial));
            StateHasChanged();
        }

        private void AddNoRecipeMedicals()
        {
            if (!IsDoctor)
            {
                return;
            }
            PatientPersonalInfo?.PatientMedicalInfo.MedicalSupport.NoRecipeMedicals.Add(new MedicalNoRecipeRecord(DoctorId.Value, "", 0, ""));
        }
        private void AddPhysicalTherapyRecords()
        {
            PatientPersonalInfo?.PatientMedicalInfo.MedicalSupport.PhysicalTherapyRecords.Add(new PhysicalTherapyRecord("", "", "", "", DateTime.UtcNow, DateTime.UtcNow));
        }
        private void AddTherapeuticExerciseMassages()
        {
            PatientPersonalInfo?.PatientMedicalInfo.MedicalSupport.TherapeuticExerciseMassages.Add(new TherapeuticExerciseMassage(""));
        }
        private void AddUnconventionalTreatmentMethods()
        {
            PatientPersonalInfo?.PatientMedicalInfo.MedicalSupport.UnconventionalTreatmentMethods.Add(new UnconventionalTreatmentMethod(""));
        }
        private void AddRadiationTherapies()
        {
            PatientPersonalInfo?.PatientMedicalInfo.MedicalSupport.RadiationTherapies.Add(new RadiationTherapy(""));
        }
        private void AddClinicalExaminations()
        {
            PatientPersonalInfo?.PatientMedicalInfo.MedicalSupport.ClinicalExaminations.Add(new ClinicalExamination(DateTime.UtcNow, "", DateTime.UtcNow, "", DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, DateTime.UtcNow, ""));
        }
    }
}