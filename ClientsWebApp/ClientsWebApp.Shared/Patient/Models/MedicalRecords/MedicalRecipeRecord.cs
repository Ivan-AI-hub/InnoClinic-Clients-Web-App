namespace ClientsWebApp.Shared.Patient.Models.MedicalRecords
{
    public class MedicalRecipeRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid DoctorId { get; set; }
        public string Name { get; set; }
        public int Dosage { get; set; }
        public string DosageMetric { get; set; }

        public MedicalRecipe RecipeInfo { get; set; }
        public double Price { get; set; }

        public bool IsPreferential { get; set; }
    }
}
