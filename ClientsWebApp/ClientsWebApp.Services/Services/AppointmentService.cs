using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using ClientsWebApp.Services.UriConstructors;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ClientsWebApp.Services.Services
{
    public class AppointmentService : BaseService, IAppointmentService
    {
        private string _baseUri;
        public AppointmentService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.AppointmentsBaseUri;
        }

        public async Task ApproveAsync(Guid id, CancellationToken cancellationToken)
        {
            var parameters = new Dictionary<string, string>();
            string requestUri = _baseUri + $"/{id}/approve";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, new FormUrlEncodedContent(parameters), cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task<Appointment> CreateAsync(CreateAppointmentModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri;
            var httpResponseMessage = await (await RequestClient).PostAsJsonAsync(requestUri, model, cancellationToken);

            return await GetFromJsonAsync<Appointment>(httpResponseMessage, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}/cancel";
            var httpResponseMessage = await (await RequestClient).DeleteAsync(requestUri, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task<IEnumerable<Appointment>> GetPageAsync(Page page, AppointmentFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{page.Size}/{page.Number}" + AppointmentUriConstructor.GenerateFiltrationQuery(filtrationModel);

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<IEnumerable<Appointment>>(httpResponseMessage, cancellationToken);
        }

        public async Task RescheduleAsync(Guid id, RescheduleAppointmentModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}/reschedule";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, model, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
    }
}
