namespace ClientsWebApp.Domain.Profiles.Patient
{
    public class Patient
    {
        public Guid Id { get; private set; }
        public HumanInfo Info { get; private set; }
        public string PhoneNumber { get; private set; }
        public Patient(Guid id, HumanInfo info, string phoneNumber)
        {
            Id = id;
            Info = info;
            PhoneNumber = phoneNumber;
        }
    }
}
