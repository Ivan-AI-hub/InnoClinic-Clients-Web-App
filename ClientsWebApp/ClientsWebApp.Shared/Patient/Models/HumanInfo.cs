namespace ClientsWebApp.Shared.Patient.Models
{
    public class HumanInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Email { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDay { get; set; }
        private HumanInfo() { }
        public HumanInfo(string email, string firstName, string lastName, string middleName, DateTime birthDay)
        {
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthDay = birthDay;
        }
    }
}
