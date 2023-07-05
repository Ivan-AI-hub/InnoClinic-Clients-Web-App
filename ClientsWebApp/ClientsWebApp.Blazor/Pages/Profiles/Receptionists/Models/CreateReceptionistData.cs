using Microsoft.AspNetCore.Http;

namespace ClientsWebApp.Blazor.Pages.Profiles.Receptionists.Models
{
    public class CreateReceptionistData
    {
        public IFormFile Picture { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string MiddleName { get; set; } = "";
        public Guid OfficeId { get; set; } = default;
        public DateTime BirthDay { get; set; } = DateTime.Now;
    }
}
