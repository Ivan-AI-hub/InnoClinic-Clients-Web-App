using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Shared.Patient.Models.MedicalRecords
{
    public class ClinicalExamination
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime StartDate { get; set; }
        public string Diagnos { get; set; }
        public DateTime DiagnosDate { get; set; }
        public string ClinicalGroup { get; set; }
        public DateTime ClinicalGroupChangeDate { get; set; }
        public DateTime LastInspectionDate { get; set; }
        public DateTime NextInspectionDate { get; set; }
        public DateTime EndDate { get; set; }
        public string EndReason { get; set; }

    }
}
