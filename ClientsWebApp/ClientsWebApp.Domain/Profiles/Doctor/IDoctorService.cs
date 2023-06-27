namespace ClientsWebApp.Domain.Profiles.Doctor
{
    public interface IDoctorService
    {
        public Task<Doctor> CreateAsync(CreateDoctorModel model, CancellationToken cancellationTokend);
        public Task UpdateAsync(Guid id, UpdateDoctorModel model, CancellationToken cancellationTokend);
        public Task UpdateStatusAsync(Guid id, WorkStatus status, CancellationToken cancellationTokend);
        public Task<IEnumerable<Doctor>> GetPageAsync(Page page, DoctorFiltrationModel filtrationModel, CancellationToken cancellationTokend);
    }
}
