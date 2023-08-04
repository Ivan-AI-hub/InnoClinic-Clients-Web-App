namespace ClientsWebApp.Domain.Profiles.Doctor
{
    public record CreateDoctorModel(CreateHumanInfo Info, string Specialization, Guid OfficeId, int CareerStartYear);
}
