using ClientsWebApp.Domain.Identity.Tokens;
using System.Net.Http.Headers;

namespace ClientsWebApp.Domain.Identity.HttpClients
{
    public class AuthorizedClient : IAuthorizedClient
    {
        private readonly HttpClient _client;
        private readonly IStorageService _storageService;

        public Uri? BaseAddress => _client.BaseAddress;

        public AuthorizedClient(HttpClient client, IStorageService storage)
        {
            _client = client;
            _storageService = storage;
        }

        public async Task<HttpClient> GetHttpClientAsync()
        {
            await SetTokenAsync(_storageService);
            return _client;
        }

        private async Task SetTokenAsync(IStorageService storage)
        {
            var token = await storage.GetAsync<SecurityToken>(nameof(SecurityToken));
            if (token != null)
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token.AccessToken);
        }
    }
}
