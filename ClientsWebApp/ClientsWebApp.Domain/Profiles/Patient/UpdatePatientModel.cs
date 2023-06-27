namespace ClientsWebApp.Domain.Profiles.Patient
{
    public record UpdatePatientModel(ImageName? Photo,
                                     string FirstName,
                                     string LastName,
                                     string MiddleName,
                                     DateTime BirthDay,
                                     string PhoneNumber);
}
