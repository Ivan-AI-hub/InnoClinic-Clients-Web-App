using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Application.Models.Offices;
using ClientsWebApp.Domain.Offices;

namespace ClientsWebApp.Application.Models.Doctors
{
    public class DoctorDTO
    {
        public Guid Id { get; private set; }
        public HumanInfoDTO Info { get; private set; }
        public string Specialization { get; private set; }
        public OfficeDTO Office { get; private set; }
        public int CareerStartYear { get; private set; }
        public WorkStatus Status { get; private set; }
        public int Experience => DateTime.UtcNow.Year - CareerStartYear + 1;
        public DoctorDTO(Guid id, HumanInfoDTO info, string specialization, OfficeDTO office, int careerStartYear, WorkStatus status)
        {
            Id = id;
            Info = info;
            Specialization = specialization;
            Office = office;
            CareerStartYear = careerStartYear;
            Status = status;
        }

        public DoctorDTO(Doctor doctor, Office office)
        {
            Id = doctor.Id;
            Info = new HumanInfoDTO(doctor.Info);
            Office = new OfficeDTO(office);
            Specialization = doctor.Specialization;
            CareerStartYear = doctor.CareerStartYear;
            Status = doctor.Status;
        }
    }
}
