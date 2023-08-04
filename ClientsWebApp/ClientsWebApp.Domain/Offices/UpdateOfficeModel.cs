namespace ClientsWebApp.Domain.Offices
{
    public record UpdateOfficeModel(ImageName? Photo,
                                    string City,
                                    string Street,
                                    int HouseNumber,
                                    int OfficeNumber,
                                    string PhoneNumber,
                                    bool Status);
}
