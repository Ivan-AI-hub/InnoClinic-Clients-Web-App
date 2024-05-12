using Blazored.Modal;
using ClientsWebApp.Application.Validators;
using ClientsWebApp.Blazor;
using ClientsWebApp.Blazor.Extensions;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Services.Settings;
using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Globalization;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddBlazoredModal();
builder.Services.ConfigureServices();
builder.Services.ConfigureManagers();
builder.Services.ConfigureHttpClient();
builder.Services.AddScoped<AuthenticationStateProvider, TokenStateProvider>();
builder.Services.AddScoped<AuthenticationStateHelper>();
builder.Services.Configure<ServicesUriSettings>(builder.Configuration.GetSection("ServicesUriSettingsConfig"));
builder.Services.AddAuthorizationCore();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDataValidator>();
builder.Services.AddLocalization();

var app = builder.Build();

await app.SetDefaultCulture(app.Services.GetRequiredService<IStorageService>());

await app.RunAsync();
