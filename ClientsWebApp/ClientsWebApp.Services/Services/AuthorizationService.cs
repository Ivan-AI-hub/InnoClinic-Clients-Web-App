﻿using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.Identity.Tokens;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ClientsWebApp.Services.Services
{
    public class AuthorizationService : BaseService, IAuthorizationService
    {
        private Uri _baseUri;
        public AuthorizationService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = new Uri(settings.Value.AuthorizationBaseUri);
        }

        public async Task ConfirmEmailAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var parameters = new Dictionary<string, string>();
            var requestUri = _baseUri + $"/confirm/{userId}";
            var httpResponseMessage = await (await RequestClient).PutAsync(requestUri, new FormUrlEncodedContent(parameters), cancellationToken);
            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task<AccessToken> SingIn(string email, string password, CancellationToken cancellationToken = default)
        {
            var quary = $"?{nameof(email)}={email}&{nameof(password)}={password}";
            var requestUri = _baseUri + $"/SingIn" + quary;

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<AccessToken>(httpResponseMessage, cancellationToken);
        }

        public async Task<User> SingUpAsync(SingUpModel model, CancellationToken cancellationToken = default)
        {
            string requestUri = _baseUri + "/singUp";
            var httpResponseMessage = await (await RequestClient).PostAsJsonAsync(requestUri, model, cancellationToken);

            return await GetFromJsonAsync<User>(httpResponseMessage, cancellationToken);
        }

        public async Task UpdateRole(string email, Role role, CancellationToken cancellationToken = default)
        {
            var request = new SendRoleRequest(role);
            var requestUri = _baseUri + $"/{email}/role";
            var httpResponseMessage = await (await RequestClient).PutAsJsonAsync(requestUri, request, cancellationToken);
            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
    }
}
