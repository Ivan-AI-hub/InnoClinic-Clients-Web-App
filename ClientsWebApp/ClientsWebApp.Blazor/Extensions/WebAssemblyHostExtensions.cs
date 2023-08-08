using ClientsWebApp.Domain.Identity;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

namespace ClientsWebApp.Blazor.Extensions
{
    public static class WebAssemblyHostExtensions
    {
        public async static Task SetDefaultCulture(this WebAssemblyHost host, IStorageService storageService)
        {
            var result = await storageService.GetStringAsync("Culture");
            CultureInfo culture;
            if (result != null)
                culture = new CultureInfo(result);
            else
                culture = new CultureInfo("en-US");

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}
