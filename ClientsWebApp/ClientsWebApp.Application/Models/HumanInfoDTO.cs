using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Profiles;

namespace ClientsWebApp.Application.Models
{
    public class HumanInfoDTO
    {
        public Task<Image?> Photo { get; set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public DateTime BirthDay { get; private set; }
        public HumanInfoDTO(string email, string firstName, string lastName, string middleName, DateTime birthDay)
        {
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

        public async Task<HumanInfo> ToHumanInfo()
        {
            var photo = await Photo;
            var image = photo is not null ? new ImageName(photo.FileName) : null;
            return new HumanInfo(image, Email, FirstName, LastName, MiddleName, BirthDay);
        }
    }
}
