using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using ClientsWebApp.Services.UriConstructors;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ClientsWebApp.Services.Services
{
    public class DoctorService : BaseService, IDoctorService
    {
        private string _baseUri;
        public DoctorService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.DoctorsBaseUri;
        }

        public async Task<Doctor> CreateAsync(CreateDoctorModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri;
            var httpResponseMessage = await (await RequestClient).PostAsJsonAsync(requestUri, model, cancellationToken);

            return await GetFromJsonAsync<Doctor>(httpResponseMessage, cancellationToken);
        }

        public async Task<Doctor> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{email}";

            var httpResponseMessage = await(await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<Doctor>(httpResponseMessage, cancellationToken);
        }

        public async Task<IEnumerable<Doctor>> GetPageAsync(Page page, DoctorFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{page.Size}/{page.Number}" + DoctorUriConstructor.GenerateFiltrationQuery(filtrationModel);

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<IEnumerable<Doctor>>(httpResponseMessage, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, UpdateDoctorModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, model, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task UpdateStatusAsync(Guid id, WorkStatus status, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>()
            {
                {"workStatus", status.ToString()}
            };
            string requestUri = _baseUri + $"/{id}/status";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, new FormUrlEncodedContent(parameters), cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
    }
}
