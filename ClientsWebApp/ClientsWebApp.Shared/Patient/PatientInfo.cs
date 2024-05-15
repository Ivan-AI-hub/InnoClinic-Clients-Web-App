using ClientsWebApp.Shared.Enums;
using ClientsWebApp.Shared.Patient.Models;

namespace ClientsWebApp.Shared.Patient
{
    public class PatientInfo
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public string EnsurenceNumber { get; set; }
        public HumanInfo? Representer { get; set; }
        public PatientMedicalInfo PatientMedicalInfo { get; set; }
    }
}
