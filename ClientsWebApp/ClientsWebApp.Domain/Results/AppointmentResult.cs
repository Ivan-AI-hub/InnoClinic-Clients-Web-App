namespace ClientsWebApp.Domain.Results
{
    public class AppointmentResult
    {
        public Guid Id { get; private set; }
        public string Complaints { get; private set; }
        public string Conclusion { get; private set; }
        public string Recomendations { get; private set; }
        public Guid AppointmentId { get; private set; }
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public AppointmentResult(Guid id, string complaints, string conclusion, string recomendations, Guid appointmentId, Guid patientId, Guid doctorId)
        {
            Id = id;
            Complaints = complaints;
            Conclusion = conclusion;
            Recomendations = recomendations;
            AppointmentId = appointmentId;
            PatientId = patientId;
            DoctorId = doctorId;
        }
    }
}
