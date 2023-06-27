namespace ClientsWebApp.Domain.Appointments
{
    public record CreateAppointmentModel(Guid PatientId,
                                         Guid DoctorId,
                                         Guid ServiceId,
                                         DateOnly Date,
                                         TimeOnly Time);
}
