namespace ClientsWebApp.Shared.Patient.Models.MedicalRecords
{
    public class RadiationTherapy
    {
        public RadiationTherapy(string name)
        {
            Name = name;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}
