namespace ClientsWebApp.Shared.Patient.Models;

public class PatientFamilyInfo
{
    public string FamilyStatus { get; set; } = string.Empty;    
    public IList<Illness> HereditaryIllnesses { get; set; } = new List<Illness>();
    public IList<Illness> PastIllnesses { get; set; } = new List<Illness>();
    public IList<RiskFactor> RiskFactors { get; set; } = new List<RiskFactor>();

}
