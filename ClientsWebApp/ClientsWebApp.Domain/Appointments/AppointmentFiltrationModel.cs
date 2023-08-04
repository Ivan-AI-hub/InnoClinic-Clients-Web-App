namespace ClientsWebApp.Domain.Appointments
{
    public class AppointmentFiltrationModel
    {
        public DateOnly Date { get; set; } = default;
        public string DoctorFirstName { get; set; } = "";
        public string DoctorMiddleName { get; set; } = "";
        public string DoctorLastName { get; set; } = "";
        public string ServiceName { get; set; } = "";
        public bool? Status { get; set; } = null;
        public Guid DoctorId { get; set; } = default;
        public Guid PatientId { get; set; } = default;
    }
}
