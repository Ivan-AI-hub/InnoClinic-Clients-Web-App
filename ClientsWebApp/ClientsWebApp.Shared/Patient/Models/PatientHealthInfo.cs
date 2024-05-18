namespace ClientsWebApp.Shared.Patient.Models;

public class PatientHealthInfo
{
    public IList<Illness> AllergicIllnesses { get; set; } = new List<Illness>();
    public IList<MedicalReaction> MedicalReactions { get; set; } = new List<MedicalReaction>();
}
