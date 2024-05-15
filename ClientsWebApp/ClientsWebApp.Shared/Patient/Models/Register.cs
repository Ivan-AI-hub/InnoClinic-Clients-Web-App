namespace ClientsWebApp.Shared.Patient.Models
{
    public class Register
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
