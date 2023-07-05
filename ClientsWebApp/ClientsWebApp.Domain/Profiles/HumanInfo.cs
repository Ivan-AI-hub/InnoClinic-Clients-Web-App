namespace ClientsWebApp.Domain.Profiles
{
    public class HumanInfo
    {
        public ImageName? Photo { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public DateTime BirthDay { get; private set; }
        public HumanInfo(ImageName? photo, string email, string firstName, string lastName, string middleName, DateTime birthDay)
        {
            Photo = photo;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthDay = birthDay;
        }

        public string GetFullName()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}
