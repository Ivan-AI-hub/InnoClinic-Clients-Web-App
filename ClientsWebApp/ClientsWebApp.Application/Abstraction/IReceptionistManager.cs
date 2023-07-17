using ClientsWebApp.Application.Models.Receptionists;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Profiles.Receptionist;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IReceptionistManager
    {
        public Task CreateAsync(CreateReceptionistData data, CancellationToken cancellationToken);
        public Task EditAsync(ReceptionistDTO oldReceptionist, EditReceptionistData data, CancellationToken cancellationToken);
        public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
        public Task<IEnumerable<ReceptionistDTO>> GetPageAsync(Page page, ReceptionistFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<IEnumerable<Receptionist>> GetInfoPageAsync(Page page, ReceptionistFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<ReceptionistDTO> GetByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<ReceptionistDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
