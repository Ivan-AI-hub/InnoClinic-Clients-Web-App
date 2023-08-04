namespace ClientsWebApp.Domain.Results
{
    public record CreateResultModel(string Complaints, string Conclusion, string Recomendations, Guid AppointmentId, Guid PatientId, Guid DoctorId);
}
