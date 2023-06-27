using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.Specializations;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ClientsWebApp.Services.Services
{
    internal class SpecializationService : BaseService, ISpecializationService
    {
        private string _baseUri;
        public SpecializationService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.SpecializationsBaseUri;
        }

        public async Task ChangeStatusAsync(Guid id, ChangeSpecializationStatusModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}/status";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, model, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task<Specialization> CreateAsync(CreateSpecializationModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri;
            var httpResponseMessage = await (await RequestClient).PostAsJsonAsync(requestUri, model, cancellationToken);

            return await GetFromJsonAsync<Specialization>(httpResponseMessage, cancellationToken);
        }

        public async Task<Specialization> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<Specialization>(httpResponseMessage, cancellationToken);
        }

        public async Task<IEnumerable<Specialization>> GetInfoAsync(Page page, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{page.Size}/{page.Number}";

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<IEnumerable<Specialization>>(httpResponseMessage, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, UpdateSpecializationModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, model, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
    }
}
