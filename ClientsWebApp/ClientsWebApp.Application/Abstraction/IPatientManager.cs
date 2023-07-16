using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Domain;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IPatientManager
    {
        public Task<ManagerResult> CreateAsync(CreatePatientData data, CancellationToken cancellationToken);
        public Task<ManagerResult> EditAsync(EditPatientData data, CancellationToken cancellationToken);
        public Task<ManagerResult> DeleteAsync(Guid id, CancellationToken cancellationToken);
        public Task<IEnumerable<Patient>> GetPageAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<Patient> GetByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<Patient> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
