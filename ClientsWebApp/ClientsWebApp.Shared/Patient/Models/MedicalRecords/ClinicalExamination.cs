using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Shared.Patient.Models.MedicalRecords
{
    public class ClinicalExamination
    {
        public ClinicalExamination(DateTime? startDate, string diagnos, DateTime? diagnosDate, string clinicalGroup, DateTime? clinicalGroupChangeDate, DateTime? lastInspectionDate, DateTime? nextInspectionDate, DateTime? endDate, string endReason)
        {
            StartDate = startDate;
            Diagnos = diagnos;
            DiagnosDate = diagnosDate;
            ClinicalGroup = clinicalGroup;
            ClinicalGroupChangeDate = clinicalGroupChangeDate;
            LastInspectionDate = lastInspectionDate;
            NextInspectionDate = nextInspectionDate;
            EndDate = endDate;
            EndReason = endReason;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime? StartDate { get; set; }
        public string Diagnos { get; set; }
        public DateTime? DiagnosDate { get; set; }
        public string ClinicalGroup { get; set; }
        public DateTime? ClinicalGroupChangeDate { get; set; }
        public DateTime? LastInspectionDate { get; set; }
        public DateTime? NextInspectionDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string EndReason { get; set; }

    }
}
