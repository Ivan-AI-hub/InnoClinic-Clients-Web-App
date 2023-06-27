using ClientsWebApp.Blazor.Infrastructure;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.HttpClients;
using ClientsWebApp.Services;

namespace ClientsWebApp.Blazor.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddScoped<IStorageService, LocalStorageService>();
        }

        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthorizedClient, AuthorizedClient>();
        }
    }
}
