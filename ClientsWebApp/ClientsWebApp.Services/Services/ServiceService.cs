using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.Services;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ClientsWebApp.Services.Services
{
    public class ServiceService : BaseService, IServiceService
    {
        private string _baseUri;
        public ServiceService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.ServicesBaseUri;
        }

        public async Task ChangeStatusAsync(Guid id, ChangeServiceStatusModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}/status";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, model, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task<Service> CreateAsync(CreateServiceModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri;
            var httpResponseMessage = await (await RequestClient).PostAsJsonAsync(requestUri, model, cancellationToken);

            return await GetFromJsonAsync<Service>(httpResponseMessage, cancellationToken);
        }

        public async Task<IEnumerable<Service>> GetByCategoryAsync(string categoryName, Page page, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{categoryName}/{page.Size}/{page.Number}";

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<IEnumerable<Service>>(httpResponseMessage, cancellationToken);
        }

        public async Task<Service> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<Service>(httpResponseMessage, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, UpdateServiceModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, model, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
    }
}
