namespace ClientsWebApp.Shared.Patient.Models;

public class PatientHealthInfo
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public IEnumerable<Illness> AllergicIllnesses { get; set; }
    public IEnumerable<MedicalReaction> MedicalReactions { get; set; }
}
