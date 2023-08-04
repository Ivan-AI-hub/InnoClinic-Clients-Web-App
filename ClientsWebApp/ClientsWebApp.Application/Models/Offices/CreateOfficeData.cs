using Microsoft.AspNetCore.Http;

namespace ClientsWebApp.Application.Models.Offices
{
    public class CreateOfficeData
    {
        public IFormFile Picture { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int OfficeNumber { get; set; }
        public string PhoneNumber { get; set; }
        public bool Status { get; set; }
    }
}
