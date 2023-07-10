using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using ClientsWebApp.Services.UriConstructors;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ClientsWebApp.Services.Services
{
    public class PatientService : BaseService, IPatientService
    {
        private string _baseUri;
        public PatientService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.PatientsBaseUri;
        }

        public async Task<Patient> CreateAsync(CreatePatientModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri;
            var httpResponseMessage = await (await RequestClient).PostAsJsonAsync(requestUri, model, cancellationToken);

            return await GetFromJsonAsync<Patient>(httpResponseMessage, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";
            var httpResponseMessage = await (await RequestClient).DeleteAsync(requestUri, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task<Patient> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/email/{email}";

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<Patient>(httpResponseMessage, cancellationToken);
        }

        public async Task<Patient> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<Patient>(httpResponseMessage, cancellationToken);
        }

        public async Task<IEnumerable<Patient>> GetPageAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{page.Size}/{page.Number}" + PatientUriConstructor.GenerateFiltrationQuery(filtrationModel);

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<IEnumerable<Patient>>(httpResponseMessage, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, UpdatePatientModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, model, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
    }
}
