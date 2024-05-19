namespace ClientsWebApp.Shared.Patient.Models
{
    public class Register
    {
        public Register(DateTime? date, string name, string status)
        {
            Date = date;
            Name = name;
            Status = status;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime? Date { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
}
