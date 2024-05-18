namespace ClientsWebApp.Shared.Patient.Models.MedicalRecords
{
    public class MedicalRecipeRecord
    {
        public MedicalRecipeRecord(Guid doctorId, string name, int dosage, string dosageMetric, MedicalRecipe recipeInfo, double price, bool isPreferential)
        {
            DoctorId = doctorId;
            Name = name;
            Dosage = dosage;
            DosageMetric = dosageMetric;
            RecipeInfo = recipeInfo;
            Price = price;
            IsPreferential = isPreferential;
        }

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
