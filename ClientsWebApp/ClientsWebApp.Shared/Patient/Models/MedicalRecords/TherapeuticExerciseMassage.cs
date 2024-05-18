using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Shared.Patient.Models.MedicalRecords
{
    public class TherapeuticExerciseMassage
    {
        public TherapeuticExerciseMassage(string name)
        {
            Name = name;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }
    }
}
