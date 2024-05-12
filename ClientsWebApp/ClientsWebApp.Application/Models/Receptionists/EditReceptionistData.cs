using Microsoft.AspNetCore.Http;

namespace ClientsWebApp.Application.Models.Receptionists
{
    public class EditReceptionistData
    {
        public IFormFile Picture { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public Guid OfficeId { get; set; }
        public DateTime? BirthDay { get; set; } = DateTime.Now;

        public EditReceptionistData(ReceptionistDTO receptionist)
        {
            FirstName = receptionist.Info.FirstName;
            LastName = receptionist.Info.LastName;
            MiddleName = receptionist.Info.MiddleName;
            OfficeId = receptionist.Office?.Id ?? default;
            BirthDay = receptionist.Info.BirthDay;
        }
    }
}
