using Microsoft.AspNetCore.Http;

namespace ClientsWebApp.Application.Models.Patients
{
    public class CreatePatientData
    {
        public IFormFile Picture { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDay { get; set; } = DateTime.Now;
    }
}
