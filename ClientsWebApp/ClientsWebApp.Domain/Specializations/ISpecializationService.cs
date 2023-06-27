namespace ClientsWebApp.Domain.Specializations
{
    public interface ISpecializationService
    {
        public Task<IEnumerable<Specialization>> GetInfoAsync(Page page, CancellationToken cancellationToken);
        public Task<Specialization> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task ChangeStatusAsync(Guid id, ChangeSpecializationStatusModel model, CancellationToken cancellationToken);
        public Task UpdateAsync(Guid id, UpdateSpecializationModel model, CancellationToken cancellationToken);
        public Task<Specialization> CreateAsync(CreateSpecializationModel model, CancellationToken cancellationToken);
    }
}
