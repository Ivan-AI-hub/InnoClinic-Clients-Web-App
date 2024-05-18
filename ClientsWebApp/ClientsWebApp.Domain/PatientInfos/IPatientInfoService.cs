using ClientsWebApp.Shared.Patient;

namespace ClientsWebApp.Domain.PatientInfos
{
    public interface IPatientInfoService
    {
        public Task<PatientPersonalInfo> CreateAsync(PatientPersonalInfo model, CancellationToken cancellationToken);
        public Task UpdateAsync(Guid id, PatientPersonalInfo model, CancellationToken cancellationToken);
        public Task DeleteAsync(Guid patientId, CancellationToken cancellationToken);
        public Task<PatientPersonalInfo> GetForPatientAsync(Guid patientId, CancellationToken cancellationToken);
    }
}
