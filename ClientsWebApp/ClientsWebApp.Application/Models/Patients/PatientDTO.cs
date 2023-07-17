using ClientsWebApp.Domain.Profiles;
using ClientsWebApp.Domain.Profiles.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Application.Models.Patients
{
    public class PatientDTO
    {
        public Guid Id { get; private set; }
        public HumanInfoDTO Info { get; private set; }
        public string PhoneNumber { get; private set; }

        public PatientDTO(Guid id, HumanInfoDTO info, string phoneNumber)
        {
            Id = id;
            Info = info;
            PhoneNumber = phoneNumber;
        }

        public PatientDTO(Patient patient)
        {
            Id = patient.Id;
            Info = new HumanInfoDTO(patient.Info);
            PhoneNumber = patient.PhoneNumber;
        }
    }
}
