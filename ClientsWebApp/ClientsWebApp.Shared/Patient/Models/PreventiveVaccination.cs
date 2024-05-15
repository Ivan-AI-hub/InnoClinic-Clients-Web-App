namespace ClientsWebApp.Shared.Patient.Models
{
    /// <summary>
    /// Профилактические прививки
    /// </summary>
    public class PreventiveVaccination
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime Date { get; set; }
        public string Medicament { get; set; }
    }
}
