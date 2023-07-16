using Microsoft.AspNetCore.Http;

namespace ClientsWebApp.Application.Models.Doctors
{
    public class CreateDoctorData
    {
        public IFormFile Picture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Specialization { get; set; }
        public Guid OfficeId { get; set; }
        public int CareerStartYear { get; set; }
        public DateTime BirthDay { get; set; } = DateTime.Now;
    }
}
