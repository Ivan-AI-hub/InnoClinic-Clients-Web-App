using ClientsWebApp.Domain.Exceptions;
using ClientsWebApp.Domain.Identity.HttpClients;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace ClientsWebApp.Services.Abstractions
{
    public abstract class BaseService
    {
        protected Task<HttpClient> RequestClient => _client.GetHttpClientAsync();
        private readonly IAuthorizedClient _client;

        public BaseService(IAuthorizedClient client)
        {
            _client = client;
        }

        protected static async Task<T> GetFromJsonAsync<T>(HttpResponseMessage message, CancellationToken cancellationToken, JsonSerializerOptions? jsonOptions = null)
        {
            await EnsureSuccessStatusCode(message, cancellationToken);
            var result = await message.Content.ReadFromJsonAsync<T>(jsonOptions, cancellationToken) ?? throw new Exception("Не успешный каст объекта");
            return result;
        }
        protected static async Task EnsureSuccessStatusCode(HttpResponseMessage message, CancellationToken cancellationToken)
        {
            if (!message.IsSuccessStatusCode)
            {
                var errorMessage = await GetErrorMessageAsync(message, cancellationToken);
                throw message.StatusCode switch
                {
                    HttpStatusCode.BadRequest => new BadRequestException(errorMessage),
                    HttpStatusCode.NotFound => new NotFoundException(errorMessage),
                    _ => new ServerErrorException(errorMessage),
                };
            }
        }

        private static async Task<string> GetErrorMessageAsync(HttpResponseMessage message, CancellationToken cancellationToken)
        {
            var error = await message.Content.ReadFromJsonAsync<ErrorModel>(cancellationToken: cancellationToken) ?? throw new Exception("Не успешный каст объекта");
            return error.Message;
        }
    }
}
