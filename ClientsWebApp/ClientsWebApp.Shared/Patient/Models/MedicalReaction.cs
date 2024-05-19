namespace ClientsWebApp.Shared.Patient.Models;

public class MedicalReaction
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string MedicalName { get; set; }
    public DateTime? Date { get; set; }
    public string ReactionDescription { get; set; }

    public MedicalReaction(string medicalName, DateTime? date, string reactionDescription)
    {
        MedicalName = medicalName;
        Date = date;
        ReactionDescription = reactionDescription;
    }

}
