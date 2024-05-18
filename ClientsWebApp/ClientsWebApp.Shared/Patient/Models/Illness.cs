namespace ClientsWebApp.Shared.Patient.Models
{
    public class Illness
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;
        public DateTime? RegistrationDate { get; set; }
        public Guid? DoctorId { get; set; }

        public Illness(string name, DateTime? registrationDate, Guid? doctorId)
        {
            Name = name;
            RegistrationDate = registrationDate;
            DoctorId = doctorId;
        }
    }
}
