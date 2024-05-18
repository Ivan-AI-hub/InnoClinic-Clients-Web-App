namespace ClientsWebApp.Shared.Patient.Models
{
    public class Aggrement
    {
        public Aggrement(string title, string description, string status)
        {
            Title = title;
            Description = description;
            Status = status;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}
