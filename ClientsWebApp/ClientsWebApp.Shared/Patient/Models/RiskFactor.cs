namespace ClientsWebApp.Shared.Patient.Models
{
    public class RiskFactor
    {
        public RiskFactor(string name)
        {
            Name = name;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}
