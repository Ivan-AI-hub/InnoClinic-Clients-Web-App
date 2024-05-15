using ClientsWebApp.Shared.Patient.Models;

namespace ClientsWebApp.Shared.Patient;

public class PatientMedicalInfo
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public PatientFamilyInfo FamilyInfo { get; set; }
    public PatientLifeInfo LifeInfo { get; set; }
    public PatientHealthInfo HealthInfo { get; set; }
    public PatientMetricInfo MetricInfo { get; set; }
    public WomanHealthInfo WomanHealthInfo { get; set; }
    public PatientPhysicInfo PhysicInfo { get; set; }
    public IEnumerable<PreventiveVaccination> PreventiveVaccinations { get; set; }
    public PatientMedicalSupport MedicalSupport { get; set; }
    public IEnumerable<IncapacityWorkCertificate> IncapacityWorkCertificates { get; set; }
    public IEnumerable<Disability> Disabilities { get; set; }
    public IEnumerable<Register> Registers { get; set; }
    public IEnumerable<Aggrement> Aggrements { get; set; }
}
