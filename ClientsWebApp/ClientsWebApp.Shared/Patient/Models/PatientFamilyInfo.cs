namespace ClientsWebApp.Shared.Patient.Models;

public class PatientFamilyInfo
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string FamilyStatus { get; set; }
    public IEnumerable<Illness> HereditaryIllnesses { get; set; }
    public IEnumerable<Illness> PastIllnesses { get; set; }
    public IEnumerable<RiskFactor> RiskFactors { get; set; }

}
