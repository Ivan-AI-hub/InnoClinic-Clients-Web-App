namespace ClientsWebApp.Domain.Profiles.Doctor
{
    public record UpdateDoctorModel(ImageName? Photo,
                                     string FirstName,
                                     string LastName,
                                     string MiddleName,
                                     DateTime BirthDay,
                                     string Specialization,
                                     Guid OfficeId,
                                     int CareerStartYear,
                                     WorkStatus Status);
}
