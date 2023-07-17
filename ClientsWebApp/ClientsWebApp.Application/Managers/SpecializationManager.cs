using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Specializations;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Services;
using ClientsWebApp.Domain.Specializations;

namespace ClientsWebApp.Application.Managers
{
    public class SpecializationManager : ISpecializationManager
    {
        private readonly ISpecializationService _specializationService;
        private readonly IServiceService _serviceService;

        public SpecializationManager(ISpecializationService specializationService, IServiceService serviceService)
        {
            _specializationService = specializationService;
            _serviceService = serviceService;
        }

        public Task ChangeStatusAsync(Guid id, bool status, CancellationToken cancellationToken)
        {
            var model = new ChangeServiceStatusModel(status);
            return _serviceService.ChangeStatusAsync(id, model, cancellationToken);
        }

        public Task<Specialization> CreateAsync(CreateSpecializationData data, CancellationToken cancellationToken)
        {
            var createModel = new CreateSpecializationModel(data.Name, data.IsActive);
            return _specializationService.CreateAsync(createModel, cancellationToken);
        }

        public Task EditAsync(Guid id, EditSpecializationData data, CancellationToken cancellationToken)
        {
            var updateModel = new UpdateSpecializationModel(data.Name, data.IsActive);
            return _specializationService.UpdateAsync(id, updateModel, cancellationToken);
        }

        public Task<Specialization> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _specializationService.GetByIdAsync(id, cancellationToken);
        }

        public Task<IEnumerable<Specialization>> GetInfoAsync(Page page, CancellationToken cancellationToken)
        {
            return _specializationService.GetInfoAsync(page, cancellationToken);
        }
    }
}
