using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Shared.Patient.Models
{
    public class Disability
    {
        public Disability(string conclusion, DateTime creationDate)
        {
            Conclusion = conclusion;
            CreationDate = creationDate;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Conclusion { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
