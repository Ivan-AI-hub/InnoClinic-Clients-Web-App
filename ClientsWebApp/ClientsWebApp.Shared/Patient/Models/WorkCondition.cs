namespace ClientsWebApp.Shared.Patient.Models
{
    public class WorkCondition
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}
