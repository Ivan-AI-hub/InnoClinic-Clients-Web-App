using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Services;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Services;

namespace ClientsWebApp.Application.Managers
{
    public class ServiceManager : IServiceManager
    {
        private readonly IServiceService _serviceService;

        public ServiceManager(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        public Task ChangeStatusAsync(Guid id, bool status, CancellationToken cancellationToken)
        {
            var model = new ChangeServiceStatusModel(status);
            return _serviceService.ChangeStatusAsync(id, model, cancellationToken);
        }

        public async Task CreateAsync(CreateServiceData data, CancellationToken cancellationToken)
        {
            var createModel = new CreateServiceModel(data.Name, data.Price, data.Status, data.SpecializationId, data.CategoryName);

            await _serviceService.CreateAsync(createModel, cancellationToken);
        }

        public async Task EditAsync(Guid id, EditServiceData data, CancellationToken cancellationToken)
        {
            var updateModel = new UpdateServiceModel(data.Name, data.Price, data.Status, data.SpecializationId, data.CategoryName);

            await _serviceService.UpdateAsync(id, updateModel, cancellationToken);
        }

        public Task<IEnumerable<Service>> GetByCategoryAsync(string categoryName, Page page, CancellationToken cancellationToken)
        {
            return _serviceService.GetByCategoryAsync(categoryName, page, cancellationToken);
        }

        public Task<Service> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _serviceService.GetByIdAsync(id, cancellationToken);
        }

        public Task<IEnumerable<Service>> GetBySpecializationAsync(string specializationName, CancellationToken cancellationToken)
        {
            return _serviceService.GetBySpecializationAsync(specializationName, cancellationToken);
        }
    }
}
