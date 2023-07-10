using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Pages.Services.Models;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Services;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Services
{
    public partial class ServicesList : CancellableComponent
    {
        [Inject] public IServiceService ServiceService { get; set; }

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
            this.StateHasChanged();

            Services = await ServiceService.GetByCategoryAsync(category, Page, _cts.Token);
            this.StateHasChanged();
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
