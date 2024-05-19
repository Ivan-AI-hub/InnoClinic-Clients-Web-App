namespace ClientsWebApp.Shared.Patient.Models
{
    public class HumanInfo
    {
        public string Email { get; private set; } = string.Empty;   
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public DateTime? BirthDay { get; set; }
        private HumanInfo() { }
        public HumanInfo(string email, string firstName, string lastName, string middleName, DateTime? birthDay)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthDay = birthDay;
        }
    }
}
