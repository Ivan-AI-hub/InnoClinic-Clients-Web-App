using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Profiles;

namespace ClientsWebApp.Application.Models
{
    public class HumanInfoDTO
    {
        public Image? Photo { get; set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public DateTime BirthDay { get; private set; }
        public HumanInfoDTO(Image? photo, string email, string firstName, string lastName, string middleName, DateTime birthDay)
        {
            Photo = photo;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthDay = birthDay;
        }

        public HumanInfoDTO(HumanInfo humanInfo)
        {
            Email = humanInfo.Email;
            FirstName = humanInfo.FirstName;
            LastName = humanInfo.LastName;
            MiddleName = humanInfo.MiddleName;
            BirthDay = humanInfo.BirthDay;
        }

        public string GetFullName()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }

        public HumanInfo ToHumanInfo()
        {
            var image = Photo != null ? new ImageName(Photo.FileName) : null;
            return new HumanInfo(image, Email, FirstName, LastName, MiddleName, BirthDay);
        }
    }
}
