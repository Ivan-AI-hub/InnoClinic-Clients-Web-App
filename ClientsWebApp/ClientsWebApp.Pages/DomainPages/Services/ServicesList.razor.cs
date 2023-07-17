using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Services;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Services;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Services
{
    public partial class ServicesList : CancellableComponent
    {
        [Inject] public IServiceManager ServiceManager { get; set; }

        private List<string> Categories = new List<string>() { "consultation", "analyses", "diagnostics" };
        private ServiceListData Data { get; set; } = new();
        private Page Page { get; set; }

        private IEnumerable<Service>? Services { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            await ServicesUpdateAsync(Categories.First());
        }

        private async Task ServicesUpdateAsync(string category)
        {
            Data.CategoryName = category;

            Services = null;
            StateHasChanged();

            Services = await ServiceManager.GetByCategoryAsync(category, Page, _cts.Token);
            StateHasChanged();
        }

        protected async Task SetPreviousPage()
        {
            Page.Number--;
            await ServicesUpdateAsync(Data.CategoryName);
        }
        protected async Task SetNextPage()
        {
            Page.Number++;
            await ServicesUpdateAsync(Data.CategoryName);
        }
        protected PageStatus GetPageStatus()
        {
            return Page.GetPageStatus(Services == null ? 0 : Services.Count());
        }
    }
}
