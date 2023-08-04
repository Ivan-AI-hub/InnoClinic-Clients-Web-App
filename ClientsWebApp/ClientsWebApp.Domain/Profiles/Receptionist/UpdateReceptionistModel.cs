namespace ClientsWebApp.Domain.Profiles.Receptionist
{
    public record UpdateReceptionistModel(ImageName? Photo,
                                     string FirstName,
                                     string LastName,
                                     string MiddleName,
                                     DateTime BirthDay,
                                     Guid OfficeId);
}
