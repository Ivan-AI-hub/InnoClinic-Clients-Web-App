﻿using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.PatientInfos;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using ClientsWebApp.Shared.Patient;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ClientsWebApp.Services.Services
{
    public class PatientInfoService : BaseService, IPatientInfoService
    {
        private string _baseUri;
        public PatientInfoService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.PatientInfoBaseUri;
        }
        public async Task<PatientPersonalInfo> CreateAsync(PatientPersonalInfo model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri;
            var httpResponseMessage = await (await RequestClient).PostAsJsonAsync(requestUri, model, cancellationToken);

            return await GetFromJsonAsync<PatientPersonalInfo>(httpResponseMessage, cancellationToken);
        }

        public async Task<PatientPersonalInfo> GetForPatientAsync(Guid patientId, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{patientId}";

            var httpResponseMessage = await(await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<PatientPersonalInfo>(httpResponseMessage, cancellationToken);
        }

        public async Task UpdateAsync(Guid id, PatientPersonalInfo model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{id}";
            var httpResponseMessage = await(await RequestClient).PutAsJsonAsync(requestUri, model, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task DeleteAsync(Guid patientId, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{patientId}";
            var httpResponseMessage = await (await RequestClient).DeleteAsync(requestUri, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
    }
}
