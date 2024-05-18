using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Shared.Patient;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IPatientManager
    {
        public Task CreateAsync(CreatePatientData data, CancellationToken cancellationToken);
        public Task EditAsync(PatientDTO oldPatient, EditPatientData data, CancellationToken cancellationToken);
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        public Task<IEnumerable<PatientDTO>> GetPageAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<IEnumerable<Patient>> GetInfoPageAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<PatientDTO> GetByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<Patient> GetInfoByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<PatientDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        public Task<PatientPersonalInfo> CreateInfoAsync(PatientPersonalInfo model, CancellationToken cancellationToken);
        public Task UpdateInfoAsync(Guid id, PatientPersonalInfo model, CancellationToken cancellationToken);
        public Task DeleteInfoAsync(Guid patientId, CancellationToken cancellationToken);
        public Task<PatientPersonalInfo> GetInfoForPatientAsync(Guid patientId, CancellationToken cancellationToken);
    }
}
