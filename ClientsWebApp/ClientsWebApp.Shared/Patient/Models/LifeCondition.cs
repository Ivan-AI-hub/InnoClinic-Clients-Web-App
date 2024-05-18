namespace ClientsWebApp.Shared.Patient.Models
{
    public class LifeCondition
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public LifeCondition(string name)
        {
            Name = name;
        }
    }
}
