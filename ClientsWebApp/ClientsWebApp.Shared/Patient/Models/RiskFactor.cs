namespace ClientsWebApp.Shared.Patient.Models
{
    public class RiskFactor
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}
