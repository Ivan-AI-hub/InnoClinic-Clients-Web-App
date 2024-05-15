using ClientsWebApp.Shared.Patient;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IPatientInfoManager
    {
        public Task<PatientInfo> CreateAsync(PatientInfo model, CancellationToken cancellationToken);
        public Task UpdateAsync(Guid id, PatientInfo model, CancellationToken cancellationToken);
        public Task<PatientInfo> GetForPatientAsync(Guid patientId, CancellationToken cancellationToken);
    }
}
