namespace ClientsWebApp.Domain.Offices
{
    public interface IOfficeService
    {
        public Task<Office> CreateAsync(CreateOfficeModel model, CancellationToken cancellationTokend);
        public Task UpdateAsync(Guid id, UpdateOfficeModel model, CancellationToken cancellationTokend);
        public Task UpdateStatusAsync(Guid id, bool newStatus, CancellationToken cancellationTokend);
        public Task<IEnumerable<Office>> GetPageAsync(Page page, CancellationToken cancellationTokend);
        public Task<Office> GetByIdAsync(Guid id, CancellationToken cancellationTokend);
    }
}
