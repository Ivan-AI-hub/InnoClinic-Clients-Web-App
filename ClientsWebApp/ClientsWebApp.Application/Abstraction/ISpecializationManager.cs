using ClientsWebApp.Application.Models.Specializations;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Specializations;

namespace ClientsWebApp.Application.Abstraction
{
    public interface ISpecializationManager
    {
        public Task<Specialization> CreateAsync(CreateSpecializationData data, CancellationToken cancellationToken);
        public Task EditAsync(Guid id, EditSpecializationData data, CancellationToken cancellationToken);
        public Task ChangeStatusAsync(Guid id, bool status, CancellationToken cancellationToken);
        public Task<IEnumerable<Specialization>> GetInfoAsync(Page page, CancellationToken cancellationToken);
        public Task<Specialization> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
