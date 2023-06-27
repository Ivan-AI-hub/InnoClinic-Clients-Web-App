namespace ClientsWebApp.Domain.Appointments
{
    public record RescheduleAppointmentModel(Guid DoctorId, DateOnly Date, TimeOnly Time);
}
