namespace ClientsWebApp.Domain.Offices
{
    public class Office
    {
        public Guid Id { get; private set; }
        public ImageName? Photo { get; private set; }
        public Address Address { get; private set; }
        public int OfficeNumber { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool Status { get; set; }

        public Office(Guid id, ImageName? photo, Address address, int officeNumber, string phoneNumber, bool status)
        {
            Id = id;
            Photo = photo;
            Address = address;
            OfficeNumber = officeNumber;
            PhoneNumber = phoneNumber;
            Status = status;
        }
    }
}
