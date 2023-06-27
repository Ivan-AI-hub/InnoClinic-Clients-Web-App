﻿using ClientsWebApp.Blazor.Infrastructure;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Services.Services;

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