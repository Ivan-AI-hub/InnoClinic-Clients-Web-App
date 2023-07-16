using ClientsWebApp.Application.Models.Specializations;
using ClientsWebApp.Domain.Specializations;
using ClientsWebApp.Domain;

namespace ClientsWebApp.Application.Abstraction
{
    internal interface ISpecializationManager
    {
        public Task<ManagerResult> CreateAsync(CreateSpecializationData data, CancellationToken cancellationToken);
        public Task<ManagerResult> EditAsync(EditSpecializationData data, CancellationToken cancellationToken);
        public Task<ManagerResult> ChangeStatusAsync(Guid id, bool status, CancellationToken cancellationToken);
        public Task<IEnumerable<Specialization>> GetInfoAsync(Page page, CancellationToken cancellationToken);
        public Task<Specialization> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    }
}
