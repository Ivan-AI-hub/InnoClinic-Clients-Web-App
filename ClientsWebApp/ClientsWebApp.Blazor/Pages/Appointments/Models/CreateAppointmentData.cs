namespace ClientsWebApp.Blazor.Pages.Appointments.Models
{
    public class CreateAppointmentData
    {
        public string Category { get; set; }
        public string Specialization { get; set; }
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public Guid ServiceId { get; set; }
        public Guid OfficeId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
    }
}
