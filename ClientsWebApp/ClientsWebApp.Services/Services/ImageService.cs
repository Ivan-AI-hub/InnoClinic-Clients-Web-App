using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;

namespace ClientsWebApp.Services.Services
{
    public class ImageService : BaseService, IImageService
    {
        private string _baseUri;
        public ImageService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.ImagesBaseUri;
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

        public async Task DeleteAsync(string imageName, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"?imageFileName={imageName}";
            var httpResponseMessage = await (await RequestClient).DeleteAsync(requestUri, cancellationToken);

            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }

        public async Task<Image> GetAsync(string imageName, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri + $"/{imageName}";

            var httpResponseMessage = await (await RequestClient).GetAsync(requestUri, cancellationToken);

            return await GetFromJsonAsync<Image>(httpResponseMessage, cancellationToken);
        }
    }
}
