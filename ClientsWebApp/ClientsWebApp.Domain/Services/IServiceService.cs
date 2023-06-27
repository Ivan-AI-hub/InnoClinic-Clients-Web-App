using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Domain.Services
{
    internal interface IServiceService
    {
        public Task<IEnumerable<Service>> GetByCategoryAsync(string categoryName, Page page, CancellationToken cancellationToken);
        public Task<Service> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task ChangeStatusAsync(Guid id, ChangeServiceStatusModel model, CancellationToken cancellationToken);
        public Task UpdateAsync(Guid id, UpdateServiceModel model, CancellationToken cancellationToken);
        public Task<Service> CreateAsync(CreateServiceModel model, CancellationToken cancellationToken);
    }
}
