namespace ClientsWebApp.Shared.Patient.Models
{
    public class Illness
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public Guid? DoctorId { get; set; }
    }
}
