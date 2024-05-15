namespace ClientsWebApp.Shared.Patient.Models
{
    public class PatientPhysicInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string BloodType { get; set; }
        public string RhFactor { get; set; }
        public IEnumerable<BloodTransfer> BloodTransfers { get; set; }
    }
}
