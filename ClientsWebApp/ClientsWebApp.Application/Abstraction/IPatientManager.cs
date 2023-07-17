using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Domain;

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
        public Task<PatientDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
