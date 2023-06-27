using ClientsWebApp.Domain.Documents;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace ClientsWebApp.Services.Services
{
    public class DocumentsService : BaseService, IDocumentService
    {
        private string _baseUri;
        public DocumentsService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.DocumentsBaseUri;
        }

        public async Task CreateAsync(IFormFile file, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri;
            using var stream = file.OpenReadStream();

            var content = new MultipartFormDataContent();
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            content.Add(new StreamContent(stream, (int)stream.Length), "file", file.FileName);

            var httpResponseMessage = await (await RequestClient).PostAsync(requestUri, content, cancellationToken);
            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task DeleteAsync(string documentName, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"?documentFileName={documentName}";
            var httpResponseMessage = await (await RequestClient).DeleteAsync(requestUri, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
        public async Task<Document> GetAsync(string documentName, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{documentName}";

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<Document>(httpResponseMessage, cancellationToken);
        }
    }
}
