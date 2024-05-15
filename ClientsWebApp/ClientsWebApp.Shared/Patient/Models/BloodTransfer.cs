namespace ClientsWebApp.Shared.Patient.Models
{
    public class BloodTransfer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string BloodType { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
