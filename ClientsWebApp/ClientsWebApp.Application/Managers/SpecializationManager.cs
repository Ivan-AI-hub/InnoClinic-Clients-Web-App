using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Specializations;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Specializations;

namespace ClientsWebApp.Application.Managers
{
    public class SpecializationManager : ISpecializationManager
    {
        private readonly ISpecializationService _specializationService;

        public SpecializationManager(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        public Task ChangeStatusAsync(Guid id, bool status, CancellationToken cancellationToken)
        {
            var model = new ChangeSpecializationStatusModel(status);
            return _specializationService.ChangeStatusAsync(id, model, cancellationToken);
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
