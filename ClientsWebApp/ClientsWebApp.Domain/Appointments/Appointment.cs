using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Domain.Services;

namespace ClientsWebApp.Domain.Appointments
{
    public class Appointment
    {
        public Guid Id { get; private set; }
        public DateOnly Date { get; private set; }
        public TimeOnly Time { get; private set; }
        public bool IsApproved { get; private set; }
        public Patient Patient { get; private set; }
        public Doctor Doctor { get; private set; }
        public Service Service { get; private set; }

        public Appointment(Guid id, DateOnly date, TimeOnly time, bool isApproved, Patient patient, Doctor doctor, Service serviceDTO)
        {
            Id = id;
            Date = date;
            Time = time;
            IsApproved = isApproved;
            Patient = patient;
            Doctor = doctor;
            Service = serviceDTO;
        }
    }
}
