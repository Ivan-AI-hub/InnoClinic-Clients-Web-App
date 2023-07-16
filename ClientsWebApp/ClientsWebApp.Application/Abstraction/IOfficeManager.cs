using ClientsWebApp.Application.Models.Offices;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IOfficeManager
    {
        public Task<ManagerResult> CreateAsync(CreateOfficeData data, CancellationToken cancellationToken);
        public Task<ManagerResult> EditAsync(EditOfficeData data, CancellationToken cancellationToken);
        public Task<ManagerResult> UpdateStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken);
        public Task<IEnumerable<Office>> GetPageAsync(Page page, CancellationToken cancellationToken);
        public Task<Office> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
