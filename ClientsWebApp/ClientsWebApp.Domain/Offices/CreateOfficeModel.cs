namespace ClientsWebApp.Domain.Offices
{
    public record CreateOfficeModel(ImageName PhotoName,
                            string City,
                            string Street,
                            int HouseNumber,
                            int OfficeNumber,
                            string PhoneNumber,
                            bool Status);

}
