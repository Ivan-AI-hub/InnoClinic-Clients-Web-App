using ClientsWebApp.Application.Models.Services;
using ClientsWebApp.Domain.Services;
using ClientsWebApp.Domain;

namespace ClientsWebApp.Application.Abstraction
{
    internal interface IServiceManager
    {
        public Task<ManagerResult> CreateAsync(CreateServiceData data, CancellationToken cancellationToken);
        public Task<ManagerResult> EditAsync(EditServiceData data, CancellationToken cancellationToken);
        public Task<ManagerResult> ChangeStatusAsync(Guid id, bool status, CancellationToken cancellationToken);
        public Task<IEnumerable<Service>> GetByCategoryAsync(string categoryName, Page page, CancellationToken cancellationToken);
        public Task<IEnumerable<Service>> GetBySpecializationAsync(string specializationName, CancellationToken cancellationToken);
        public Task<Service> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
