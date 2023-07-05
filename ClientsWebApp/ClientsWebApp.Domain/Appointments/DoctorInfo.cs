namespace ClientsWebApp.Domain.Appointments
{
    public class DoctorInfo
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MiddleName { get; private set; }
        public DoctorInfo(Guid id, string firstName, string lastName, string middleName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
        }

        public string GetFullName()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}
