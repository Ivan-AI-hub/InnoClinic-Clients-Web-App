using ClientsWebApp.Application.Models.Offices;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Offices;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IOfficeManager
    {
        public Task CreateAsync(CreateOfficeData data, CancellationToken cancellationToken);
        public Task EditAsync(OfficeDTO oldOffice, EditOfficeData data, CancellationToken cancellationToken);
        public Task UpdateStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken);
        public Task<IEnumerable<OfficeDTO>> GetPageAsync(Page page, CancellationToken cancellationToken);
        public Task<IEnumerable<Office>> GetInfoPageAsync(Page page, CancellationToken cancellationToken);
        public Task<OfficeDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
