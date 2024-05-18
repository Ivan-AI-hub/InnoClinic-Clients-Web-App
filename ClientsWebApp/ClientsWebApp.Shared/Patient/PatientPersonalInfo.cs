using ClientsWebApp.Shared.Enums;
using ClientsWebApp.Shared.Patient.Models;

namespace ClientsWebApp.Shared.Patient
{
    public class PatientPersonalInfo
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; } = string.Empty;
        public string EnsurenceNumber { get; set; } = string.Empty;
        public HumanInfo? Representer { get; set; }
        public PatientMedicalInfo PatientMedicalInfo { get; set; } = new PatientMedicalInfo();

        public PatientPersonalInfo(Guid personId)
        {
            PersonId = personId;
        }
    }
}
