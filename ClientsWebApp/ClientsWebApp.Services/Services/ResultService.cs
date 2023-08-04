using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.Results;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ClientsWebApp.Services.Services
{
    public class ResultService : BaseService, IResultService
    {
        private string _baseUri;
        public ResultService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.ResultsBaseUri;
        }

        public async Task<AppointmentResult> CreateAsync(CreateResultModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri;
            var httpResponseMessage = await (await RequestClient).PostAsJsonAsync(requestUri, model, cancellationToken);

            return await GetFromJsonAsync<AppointmentResult>(httpResponseMessage, cancellationToken);
        }

        public async Task<AppointmentResult> GetForAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{appointmentId}";

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<AppointmentResult>(httpResponseMessage, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, UpdateResultModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, model, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
    }
}
