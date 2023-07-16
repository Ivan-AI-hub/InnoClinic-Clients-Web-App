using ClientsWebApp.Application.Models.Receptionists;
using ClientsWebApp.Domain.Profiles.Receptionist;
using ClientsWebApp.Domain;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IReceptionistManager
    {
        public Task<ManagerResult> CreateAsync(CreateReceptionistData data, CancellationToken cancellationToken);
        public Task<ManagerResult> EditAsync(EditReceptionistData data, CancellationToken cancellationToken);
        public Task<ManagerResult> DeleteAsync(Guid id, CancellationToken cancellationToken);
        public Task<IEnumerable<Receptionist>> GetPageAsync(Page page, ReceptionistFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<Receptionist> GetByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<Receptionist> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
