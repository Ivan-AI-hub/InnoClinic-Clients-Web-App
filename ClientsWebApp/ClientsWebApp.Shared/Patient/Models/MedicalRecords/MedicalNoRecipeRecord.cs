using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Shared.Patient.Models.MedicalRecords
{
    public class MedicalNoRecipeRecord
    {
        public MedicalNoRecipeRecord(Guid doctorId, string name, int dosage, string dosageMetric)
        {
            DoctorId = doctorId;
            Name = name;
            Dosage = dosage;
            DosageMetric = dosageMetric;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid DoctorId { get; set; }
        public string Name { get; set; }
        public int Dosage { get; set; }
        public string DosageMetric { get; set; }
    }
}
