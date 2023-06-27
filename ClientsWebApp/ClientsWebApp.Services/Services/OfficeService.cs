using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ClientsWebApp.Services.Services
{
    public class OfficeService : BaseService, IOfficeService
    {
        private string _baseUri;
        public OfficeService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.OfficesBaseUri;
        }

        public async Task<Office> CreateAsync(CreateOfficeModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri;
            var httpResponseMessage = await (await RequestClient).PostAsJsonAsync(requestUri, model, cancellationToken);

            return await GetFromJsonAsync<Office>(httpResponseMessage, cancellationToken);
        }

        public async Task<Office> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<Office>(httpResponseMessage, cancellationToken);
        }

        public async Task<IEnumerable<Office>> GetPageAsync(Page page, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{page.Size}/{page.Number}";

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<IEnumerable<Office>>(httpResponseMessage, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, UpdateOfficeModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, model, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task UpdateStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"status", newStatus.ToString()}
            };

            string requestUri = _baseUri + $"/{id}/status";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, new FormUrlEncodedContent(parameters), cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
    }
}
