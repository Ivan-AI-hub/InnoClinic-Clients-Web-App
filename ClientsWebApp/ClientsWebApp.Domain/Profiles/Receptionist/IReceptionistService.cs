namespace ClientsWebApp.Domain.Profiles.Receptionist
{
    public interface IReceptionistService
    {
        public Task<Receptionist> CreateAsync(CreateReceptionistModel model, CancellationToken cancellationToken);
        public Task UpdateAsync(Guid id, UpdateReceptionistModel model, CancellationToken cancellationToken);
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        public Task<IEnumerable<Receptionist>> GetPageAsync(Page page, ReceptionistFiltrationModel filtrationModel, CancellationToken cancellationToken);
    }
}
