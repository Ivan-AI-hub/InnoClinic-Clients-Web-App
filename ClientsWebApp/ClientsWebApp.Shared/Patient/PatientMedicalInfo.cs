using ClientsWebApp.Shared.Patient.Models;

namespace ClientsWebApp.Shared.Patient;

public class PatientMedicalInfo
{
    public PatientFamilyInfo FamilyInfo { get; set; } = new PatientFamilyInfo();    
    public PatientLifeInfo LifeInfo { get; set; } = new PatientLifeInfo();
    public PatientHealthInfo HealthInfo { get; set; } = new PatientHealthInfo();
    public PatientMetricInfo MetricInfo { get; set; } = new PatientMetricInfo();
    public WomanHealthInfo WomanHealthInfo { get; set; } = new WomanHealthInfo();
    public PatientPhysicInfo PhysicInfo { get; set; } = new PatientPhysicInfo();
    public IList<PreventiveVaccination> PreventiveVaccinations { get; set; } = new List<PreventiveVaccination>();
    public PatientMedicalSupport MedicalSupport { get; set; } = new PatientMedicalSupport();
    public IList<IncapacityWorkCertificate> IncapacityWorkCertificates { get; set; } = new List<IncapacityWorkCertificate>();
    public IList<Disability> Disabilities { get; set; } = new List<Disability>();
    public IList<Register> Registers { get; set; } = new List<Register>();
    public IList<Aggrement> Aggrements { get; set; } = new List<Aggrement>();
}
