namespace ClientsWebApp.Shared.Patient.Models
{
    public class PatientPhysicInfo
    {
        public string BloodType { get; set; } = string.Empty;       
        public string RhFactor { get; set; } = string.Empty;
        public IList<BloodTransfer> BloodTransfers { get; set; } = new List<BloodTransfer>();
    }
}
