using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Domain.PatientInfos;
using ClientsWebApp.Shared.Patient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Application.Managers
{
    public class PatientInfoManager : IPatientInfoManager
    {
        private readonly IPatientInfoService _patientInfoService;

        public PatientInfoManager(IPatientInfoService patientInfoService)
        {
            _patientInfoService = patientInfoService;
        }

        public Task<PatientInfo> CreateAsync(PatientInfo model, CancellationToken cancellationToken)
        {
            return _patientInfoService.CreateAsync(model, cancellationToken);
        }

        public Task<PatientInfo> GetForPatientAsync(Guid patientId, CancellationToken cancellationToken)
        {
            return _patientInfoService.GetForPatientAsync(patientId, cancellationToken);    
        }

        public Task UpdateAsync(Guid id, PatientInfo model, CancellationToken cancellationToken)
        {
            return _patientInfoService.UpdateAsync(id, model, cancellationToken);
        }
    }
}
