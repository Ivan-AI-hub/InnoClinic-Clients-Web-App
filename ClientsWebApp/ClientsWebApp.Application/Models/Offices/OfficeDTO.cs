using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;

namespace ClientsWebApp.Application.Models.Offices
{
    public class OfficeDTO
    {
        public Guid Id { get; private set; }
        public Task<Image>? Photo { get; set; }
        public Address Address { get; private set; }
        public int OfficeNumber { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool Status { get; set; }

        public OfficeDTO(Guid id, Address address, int officeNumber, string phoneNumber, bool status)
        {
            Id = id;
            Address = address;
            OfficeNumber = officeNumber;
            PhoneNumber = phoneNumber;
            Status = status;
        }
        public OfficeDTO(Office office)
        {
            Id = office.Id;
            Address = office.Address;
            OfficeNumber = office.OfficeNumber;
            PhoneNumber = office.PhoneNumber;
            Status = office.Status;
        }

        public async Task<Office> ToOfficeAsync()
        {
            var image = Photo is not null ? new ImageName((await Photo).FileName) : null;
            return new Office(Id, image, Address, OfficeNumber, PhoneNumber, Status);
        }
    }
}
