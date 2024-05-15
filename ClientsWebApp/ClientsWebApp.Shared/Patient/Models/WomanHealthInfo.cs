namespace ClientsWebApp.Shared.Patient.Models
{
    public class WomanHealthInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime FirstMenstruationDate { get; set; }
        public DateTime LastMenstruationDate { get; set; }
        public DateTime MenstruationPeriodInDays { get; set; }
        public string MenstuationIntensivity { get; set; }
        public string MenstuationIllness { get; set; }
        public IEnumerable<WomanPregnancy> Pregnancies { get; set; }
    }
}
