namespace ClientsWebApp.Shared.Patient.Models.MedicalRecords
{
    public class MedicalRecipe
    {
        public MedicalRecipe(string serie, string number, DateTime? date)
        {
            Serie = serie;
            Number = number;
            Date = date;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Serie { get; set; }
        public string Number { get; set; }
        public DateTime? Date { get; set; }
    }
}
