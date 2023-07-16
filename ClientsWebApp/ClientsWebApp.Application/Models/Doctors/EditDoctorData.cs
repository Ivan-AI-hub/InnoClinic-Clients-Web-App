using ClientsWebApp.Domain.Profiles.Doctor;
using Microsoft.AspNetCore.Http;

namespace ClientsWebApp.Application.Models.Doctors
{
    public class EditDoctorData
    {
        public IFormFile? Picture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Specialization { get; set; }
        public Guid OfficeId { get; set; }
        public int CareerStartYear { get; set; }
        public WorkStatus Status { get; set; }
        public DateTime BirthDay { get; set; } = DateTime.Now;

        public EditDoctorData(Doctor doctor)
        {
            FirstName = doctor.Info.FirstName;
            LastName = doctor.Info.LastName;
            MiddleName = doctor.Info.MiddleName;
            Specialization = doctor.Specialization;
            OfficeId = doctor.Office?.Id ?? default;
            CareerStartYear = doctor.CareerStartYear;
            Status = doctor.Status;
            BirthDay = doctor.Info.BirthDay;
        }
    }
}
