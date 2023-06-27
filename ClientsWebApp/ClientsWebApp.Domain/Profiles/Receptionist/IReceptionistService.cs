namespace ClientsWebApp.Domain.Profiles.Receptionist
{
    public interface IReceptionistService
    {
        public Task<Receptionist> CreateAsync(CreateReceptionistModel model, CancellationToken cancellationTokend);
        public Task UpdateAsync(Guid id, UpdateReceptionistModel model, CancellationToken cancellationTokend);
        public Task DeleteAsync(Guid id, CancellationToken cancellationTokend);
        public Task<IEnumerable<Receptionist>> GetPageAsync(Page page, ReceptionistFiltrationModel filtrationModel, CancellationToken cancellationTokend);
    }
}
