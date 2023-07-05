using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Domain.Profiles.Receptionist;
using Microsoft.AspNetCore.Http;

namespace ClientsWebApp.Blazor.Pages.Profiles.Receptionists.Models
{
    public class EditReceptionistData
    {
        public IFormFile Picture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid OfficeId { get; set; }
        public DateTime BirthDay { get; set; } = DateTime.Now;

        public EditReceptionistData(Receptionist receptionist)
        {
            FirstName = receptionist.Info.FirstName;
            LastName = receptionist.Info.LastName;
            MiddleName = receptionist.Info.MiddleName;
            OfficeId = receptionist.Office.Id;
            BirthDay = receptionist.Info.BirthDay;
        }
    }
}
