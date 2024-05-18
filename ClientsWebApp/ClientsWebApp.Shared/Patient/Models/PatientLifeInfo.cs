namespace ClientsWebApp.Shared.Patient.Models;

public class PatientLifeInfo
{
    public IList<BodyFeature> BodyFeatures { get; set; } = new List<BodyFeature>();
    public IList<LifeCondition> LifeConditions { get; set; } = new List<LifeCondition>();
    public IList<WorkCondition> WorkConditions { get; set; } = new List<WorkCondition>();
}
