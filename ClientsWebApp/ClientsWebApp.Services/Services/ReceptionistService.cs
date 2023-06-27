using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.Profiles.Receptionist;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using ClientsWebApp.Services.UriConstructors;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ClientsWebApp.Services.Services
{
    public class ReceptionistService : BaseService, IReceptionistService
    {
        private string _baseUri;
        public ReceptionistService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.ReceptionistsBaseUri;
        }

        public async Task<Receptionist> CreateAsync(CreateReceptionistModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri;
            var httpResponseMessage = await (await RequestClient).PostAsJsonAsync(requestUri, model, cancellationToken);

            return await GetFromJsonAsync<Receptionist>(httpResponseMessage, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";
            var httpResponseMessage = await (await RequestClient).DeleteAsync(requestUri, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task<IEnumerable<Receptionist>> GetPageAsync(Page page, ReceptionistFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{page.Size}/{page.Number}" + ReceptionistUriConstructor.GenerateFiltrationQuery(filtrationModel);

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<IEnumerable<Receptionist>>(httpResponseMessage, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, UpdateReceptionistModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, model, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
    }
}
