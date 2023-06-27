namespace ClientsWebApp.Domain.Profiles.Patient
{
    public interface IPatientService
    {
        public Task<Patient> CreateAsync(CreatePatientModel model, CancellationToken cancellationToken);
        public Task UpdateAsync(Guid id, UpdatePatientModel model, CancellationToken cancellationToken);
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        public Task<IEnumerable<Patient>> GetPageAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<Patient> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }
}
