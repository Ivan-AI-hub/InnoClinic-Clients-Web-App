using ClientsWebApp.Shared.Patient.Models.MedicalRecords;

namespace ClientsWebApp.Shared.Patient.Models;

public class PatientMedicalSupport
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public IEnumerable<MedicalRecipeRecord> RecipeMedicals { get; set; }
    public IEnumerable<MedicalNoRecipeRecord> NoRecipeMedicals { get; set; }
    public IEnumerable<PhysicalTherapyRecord> PhysicalTherapyRecords { get; set; }
    public IEnumerable<TherapeuticExerciseMassage> TherapeuticExerciseMassages { get; set; }
    public IEnumerable<UnconventionalTreatmentMethod> UnconventionalTreatmentMethods { get; set; }
    public IEnumerable<RadiationTherapy> RadiationTherapies { get; set; }
    public IEnumerable<ClinicalExamination> ClinicalExaminations { get; set; }

}
