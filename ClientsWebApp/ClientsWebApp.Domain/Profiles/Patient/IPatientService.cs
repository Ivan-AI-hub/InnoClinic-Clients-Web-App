namespace ClientsWebApp.Domain.Profiles.Patient
{
    public interface IPatientService
    {
        public Task<Patient> CreateAsync(CreatePatientModel model, CancellationToken cancellationTokend);
        public Task UpdateAsync(Guid id, UpdatePatientModel model, CancellationToken cancellationTokend);
        public Task DeleteAsync(Guid id, CancellationToken cancellationTokend);
        public Task<IEnumerable<Patient>> GetPageAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationTokend);
    }
}
