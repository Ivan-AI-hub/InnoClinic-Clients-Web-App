namespace ClientsWebApp.Domain.Offices
{
    public interface IOfficeService
    {
        public Task<Office> CreateAsync(CreateOfficeModel model, CancellationToken cancellationToken);
        public Task UpdateAsync(Guid id, UpdateOfficeModel model, CancellationToken cancellationToken);
        public Task UpdateStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken);
        public Task<IEnumerable<Office>> GetPageAsync(Page page, CancellationToken cancellationToken);
        public Task<Office> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
