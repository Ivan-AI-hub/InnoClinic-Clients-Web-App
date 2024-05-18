using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Shared.Patient.Models.MedicalRecords;

public class PhysicalTherapyRecord
{
    public PhysicalTherapyRecord(string name, string amount, string dosage, string dosageMetric, DateTime startDate, DateTime endDate)
    {
        Name = name;
        Amount = amount;
        Dosage = dosage;
        DosageMetric = dosageMetric;
        StartDate = startDate;
        EndDate = endDate;
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }
    public string Amount { get; set; }
    public string Dosage { get; set; }
    public string DosageMetric { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
