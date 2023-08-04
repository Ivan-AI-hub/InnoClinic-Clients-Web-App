using ClientsWebApp.Domain.Categories;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Services.Abstractions;
using ClientsWebApp.Services.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace ClientsWebApp.Services.Services
{
    public class CategoryService : BaseService, ICategoryService
    {
        private string _baseUri;
        public CategoryService(IAuthorizedClient client, IOptions<ServicesUriSettings> settings) : base(client)
        {
            _baseUri = settings.Value.CategoriesBaseUri;
        }

        public async Task CreateAsync(CreateCategoryModel model, CancellationToken cancellationToken)
        {
            string requestUri = _baseUri;
            var httpResponseMessage = await (await RequestClient).PostAsJsonAsync(requestUri, model, cancellationToken);
            await EnsureSuccessStatusCode(httpResponseMessage, cancellationToken);
        }
    }
}
