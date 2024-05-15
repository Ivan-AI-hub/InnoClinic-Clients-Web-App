namespace ClientsWebApp.Shared.Patient.Models;

public class PatientLifeInfo
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public IEnumerable<BodyFeature> BodyFeatures { get; set; }
    public IEnumerable<LifeCondition> LifeConditions { get; set; }
    public IEnumerable<WorkCondition> WorkConditions { get; set; }
}
