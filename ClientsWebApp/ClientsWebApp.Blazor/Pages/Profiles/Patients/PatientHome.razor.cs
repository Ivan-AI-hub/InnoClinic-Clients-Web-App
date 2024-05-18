using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Exceptions;
using ClientsWebApp.Shared.Patient;
using ClientsWebApp.Shared.Patient.Models;
using ClientsWebApp.Shared.Patient.Models.MedicalRecords;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Patients
{
    [Authorize(Roles = "Patient")]
    public partial class PatientHome : CancellableComponent
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Inject] AuthenticationStateHelper StateHelper { get; set; }
        [Inject] IPatientManager PatientManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] private IAppointmentManager _appointmentManager { get; set; }
        private IEnumerable<Appointment>? Appointments { get; set; }
        private Page Page { get; set; } = new Page(20, 1);

        private PatientDTO? Patient { get; set; }
        private PatientPersonalInfo? PatientPersonalInfo { get; set; }
        protected override async void OnInitialized()
        {
            var email = await StateHelper.GetEmailAsync();
            try
            {
                Patient = await PatientManager.GetByEmailAsync(email, _cts.Token);
            }
            catch (NotFoundException)
            {
                NavigateToCreatePage();
            }


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

        private void NavigateToDeletePage()
        {
            Modal.Show<DeletePatient>("Delete profile");
            //NavigationManager.NavigateTo("/patients/delete");
        }

        private void NavigateToCreatePage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large,
                DisableBackgroundCancel = true,
            };
            Modal.Show<CreatePatient>("Create profile", options);
            //NavigationManager.NavigateTo("/patients/create");
        }
        private void NavigateToEditPage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large
            };
            var parameters = new ModalParameters();
            parameters.Add(nameof(EditPatient.OldPatient), Patient);
            Modal.Show<EditPatient>("Edit profile", parameters, options);
            //NavigationManager.NavigateTo("/patients/edit");
        }
        private void NavigateToHistoryPage()
        {
            NavigationManager.NavigateTo($"/patients/{Patient.Id}/appointmentsHistory");
        }


        private void AddRepresenter()
        {
            PatientPersonalInfo.Representer = new HumanInfo("", "", "", "", DateTime.Now);
        }

        private void AddHereditaryIllness()
        {
            PatientPersonalInfo?.PatientMedicalInfo.FamilyInfo.HereditaryIllnesses.Add(new Illness("", DateTime.Now, null));
        }

        private void AddPastIllness()
        {
            PatientPersonalInfo?.PatientMedicalInfo.FamilyInfo.PastIllnesses.Add(new Illness("", DateTime.Now, null));
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
            PatientPersonalInfo?.PatientMedicalInfo.HealthInfo.AllergicIllnesses.Add(new Illness("", DateTime.Now, null));
        }
        private void AddMedicalReaction()
        {
            PatientPersonalInfo?.PatientMedicalInfo.HealthInfo.MedicalReactions.Add(new MedicalReaction("", DateTime.Now, ""));
        }
        private void AddPregnancy()
        {
            PatientPersonalInfo?.PatientMedicalInfo.WomanHealthInfo.Pregnancies.Add(new WomanPregnancy(DateTime.Now, DateTime.Now, ""));
        }

        private void AddBloodTransfer()
        {
            PatientPersonalInfo?.PatientMedicalInfo.PhysicInfo.BloodTransfers.Add(new BloodTransfer("", 0, DateTime.Now));
        }

        private void AddPreventiveVactination()
        {
            PatientPersonalInfo?.PatientMedicalInfo.PreventiveVaccinations.Add(new PreventiveVaccination(DateTime.Now, ""));
        }

        private void AddIncapacityWorkCertificates()
        {
            PatientPersonalInfo?.PatientMedicalInfo.IncapacityWorkCertificates.Add(new IncapacityWorkCertificate("", "", "", DateTime.Now, DateTime.Now));
        }

        private void AddDisability()
        {
            PatientPersonalInfo?.PatientMedicalInfo.Disabilities.Add(new Disability("", DateTime.UtcNow));
        }

        private void AddRegister()
        {
            PatientPersonalInfo?.PatientMedicalInfo.Registers.Add(new Register(DateTime.Now, "", ""));
        }

        private void AddAggrement()
        {
            PatientPersonalInfo?.PatientMedicalInfo.Aggrements.Add(new Aggrement("", "", ""));
        }

        private void AddRecipeMedicals()
        {
            PatientPersonalInfo?.PatientMedicalInfo.MedicalSupport.RecipeMedicals.Add(new MedicalRecipeRecord(new Guid(), "", 0, "", new MedicalRecipe("", "", DateTime.Now), 0, true));
        }

        private void AddNoRecipeMedicals()
        {
            PatientPersonalInfo?.PatientMedicalInfo.MedicalSupport.NoRecipeMedicals.Add(new MedicalNoRecipeRecord(new Guid(), "", 0, ""));
        }
        private void AddPhysicalTherapyRecords()
        {
            PatientPersonalInfo?.PatientMedicalInfo.MedicalSupport.PhysicalTherapyRecords.Add(new PhysicalTherapyRecord("", "", "", "", DateTime.Now, DateTime.Now));
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
            PatientPersonalInfo?.PatientMedicalInfo.MedicalSupport.ClinicalExaminations.Add(new ClinicalExamination(DateTime.Now, "", DateTime.Now, "", DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, ""));
        }
    }
}
