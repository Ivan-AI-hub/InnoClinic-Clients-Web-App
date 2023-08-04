using Microsoft.AspNetCore.Http;

namespace ClientsWebApp.Application.Models.Patients
{
    public class EditPatientData
    {
        public IFormFile Picture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDay { get; set; } = DateTime.Now;

        public EditPatientData(PatientDTO patient)
        {
            FirstName = patient.Info.FirstName;
            LastName = patient.Info.LastName;
            MiddleName = patient.Info.MiddleName;
            PhoneNumber = patient.PhoneNumber;
            BirthDay = patient.Info.BirthDay;
        }
    }
}
