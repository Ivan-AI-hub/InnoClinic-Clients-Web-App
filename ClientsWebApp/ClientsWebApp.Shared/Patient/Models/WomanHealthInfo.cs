namespace ClientsWebApp.Shared.Patient.Models
{
    public class WomanHealthInfo
    {
        public DateTime? FirstMenstruationDate { get; set; }
        public DateTime? LastMenstruationDate { get; set; }
        public DateTime? MenstruationPeriodInDays { get; set; }
        public string MenstuationIntensivity { get; set; } = string.Empty;      
        public string MenstuationIllness { get; set; } = string.Empty;
        public IList<WomanPregnancy> Pregnancies { get; set; } = new List<WomanPregnancy>();
    }
}
