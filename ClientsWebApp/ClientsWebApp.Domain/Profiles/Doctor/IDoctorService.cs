namespace ClientsWebApp.Domain.Profiles.Doctor
{
    public interface IDoctorService
    {
        public Task<Doctor> CreateAsync(CreateDoctorModel model, CancellationToken cancellationToken);
        public Task UpdateAsync(Guid id, UpdateDoctorModel model, CancellationToken cancellationToken);
        public Task UpdateStatusAsync(Guid id, ChangeDoctorStatusModel model, CancellationToken cancellationToken);
        public Task<IEnumerable<Doctor>> GetPageAsync(Page page, DoctorFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<Doctor> GetByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<Doctor> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
