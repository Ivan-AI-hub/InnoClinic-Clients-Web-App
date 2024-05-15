namespace ClientsWebApp.Shared.Patient.Models.MedicalRecords
{
    public class RadiationTherapy
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}
