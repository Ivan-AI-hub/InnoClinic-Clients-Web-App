using Blazored.Modal;
using ClientsWebApp.Application.Validators;
using ClientsWebApp.Blazor;
using ClientsWebApp.Blazor.Extensions;
using ClientsWebApp.Services.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredModal();
builder.Services.ConfigureServices();
builder.Services.ConfigureManagers();
builder.Services.ConfigureHttpClient();
builder.Services.AddScoped<AuthenticationStateProvider, TokenStateProvider>();
builder.Services.AddScoped<AuthenticationStateHelper>();

builder.Services.Configure<ServicesUriSettings>(builder.Configuration.GetSection("ServicesUriSettingsConfig"));
builder.Services.AddAuthorizationCore();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDataValidator>();
await builder.Build().RunAsync();
