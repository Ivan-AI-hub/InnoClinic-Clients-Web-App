using ClientsWebApp.Domain.Exceptions;
using ClientsWebApp.Domain.HttpClients;
using System.Net.Http.Json;

namespace ClientsWebApp.Services
{
    public abstract class BaseService
    {
        protected Task<HttpClient> RequestClient => _client.GetHttpClientAsync();
        private IAuthorizedClient _client;

        public BaseService(IAuthorizedClient client)
        {
            _client = client;
        }

        protected async Task<T> GetFromJsonAsync<T>(HttpResponseMessage message, CancellationToken cancellationToken)
        {
            await EnsureSuccessStatusCode(message, cancellationToken);
            var result = await message.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
            if (result == null)
            {
                throw new Exception("Не успешный каст объекта");
            }
            return result;
        }
        protected async Task EnsureSuccessStatusCode(HttpResponseMessage message, CancellationToken cancellationToken)
        {
            if (!message.IsSuccessStatusCode)
            {
                throw new BadRequestException(await GetErrorMessageAsync(message, cancellationToken));
            }
        }

        private async Task<string> GetErrorMessageAsync(HttpResponseMessage message, CancellationToken cancellationToken)
        {
            var error = await message.Content.ReadFromJsonAsync<ErrorModel>(cancellationToken: cancellationToken);
            if (error == null)
            {
                throw new Exception("Не успешный каст объекта");
            }
            return error.Message;
        }
    }
}
