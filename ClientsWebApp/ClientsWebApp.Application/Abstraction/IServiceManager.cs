using ClientsWebApp.Application.Models.Services;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Services;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IServiceManager
    {
        public Task CreateAsync(CreateServiceData data, CancellationToken cancellationToken);
        public Task EditAsync(Guid id, EditServiceData data, CancellationToken cancellationToken);
        public Task ChangeStatusAsync(Guid id, bool status, CancellationToken cancellationToken);
        public Task<IEnumerable<Service>> GetByCategoryAsync(string categoryName, Page page, CancellationToken cancellationToken);
        public Task<IEnumerable<Service>> GetBySpecializationAsync(string specializationName, CancellationToken cancellationToken);
        public Task<Service> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
