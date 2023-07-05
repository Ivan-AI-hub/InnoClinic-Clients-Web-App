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
        public PatientInfo Patient { get; private set; }
        public DoctorInfo Doctor { get; private set; }
        public ServiceInfo Service { get; private set; }

        public Appointment(Guid id, DateOnly date, TimeOnly time, bool isApproved, PatientInfo patient, DoctorInfo doctor, ServiceInfo service)
        {
            Id = id;
            Date = date;
            Time = time;
            IsApproved = isApproved;
            Patient = patient;
            Doctor = doctor;
            Service = service;
        }
    }
}
