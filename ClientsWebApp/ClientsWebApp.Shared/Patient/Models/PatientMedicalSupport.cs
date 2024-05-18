using ClientsWebApp.Shared.Patient.Models.MedicalRecords;

namespace ClientsWebApp.Shared.Patient.Models;

public class PatientMedicalSupport
{
    public IList<MedicalRecipeRecord> RecipeMedicals { get; set; } = new List<MedicalRecipeRecord>();
    public IList<MedicalNoRecipeRecord> NoRecipeMedicals { get; set; } = new List<MedicalNoRecipeRecord>();
    public IList<PhysicalTherapyRecord> PhysicalTherapyRecords { get; set; } = new List<PhysicalTherapyRecord>();
    public IList<TherapeuticExerciseMassage> TherapeuticExerciseMassages { get; set; } = new List<TherapeuticExerciseMassage>();
    public IList<UnconventionalTreatmentMethod> UnconventionalTreatmentMethods { get; set; } = new List<UnconventionalTreatmentMethod>();
    public IList<RadiationTherapy> RadiationTherapies { get; set; } = new List<RadiationTherapy>();
    public IList<ClinicalExamination> ClinicalExaminations { get; set; } = new List<ClinicalExamination>();

}
