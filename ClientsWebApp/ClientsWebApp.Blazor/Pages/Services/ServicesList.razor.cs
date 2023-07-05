using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain;
using Microsoft.AspNetCore.Components;
using ClientsWebApp.Domain.Services;

namespace ClientsWebApp.Blazor.Pages.Services
{
    public partial class ServicesList : CancellableComponent
    {
        [Inject] public IServiceService ServiceService { get; set; }

        private string CategoryName { get; set; }
        private Page Page { get; set; }

        private IEnumerable<Service>? Services { get; set; }
        protected override async Task OnInitializedAsync()
        {
            CategoryName = "consultations";
            Page = new Page(15, 1);
            Services = await ServiceService.GetByCategoryAsync(CategoryName, Page, _cts.Token);
        }
    }
}
