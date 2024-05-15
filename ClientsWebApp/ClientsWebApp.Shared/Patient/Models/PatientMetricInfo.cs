namespace ClientsWebApp.Shared.Patient.Models
{
    public class PatientMetricInfo
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Height { get; set; }
        public string Weight { get; set; }
        public string NormalLowerPressure { get; set; }
        public string NormalHigherPressure { get; set; }
        public string NormalPulse { get; set; }
        public string NormalTemperature { get; set; }
    }
}
