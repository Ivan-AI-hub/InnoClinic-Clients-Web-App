namespace ClientsWebApp.Shared.Patient.Models
{
    public class IncapacityWorkCertificate
    {
        public IncapacityWorkCertificate(string documentNumber, string incapacityType, string incapacityReason, DateTime? startDate, DateTime? endDate)
        {
            DocumentNumber = documentNumber;
            IncapacityType = incapacityType;
            IncapacityReason = incapacityReason;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string DocumentNumber { get; set; }
        public string IncapacityType { get; set; }
        public string IncapacityReason { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
