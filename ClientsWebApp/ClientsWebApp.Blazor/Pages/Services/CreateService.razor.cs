using ClientsWebApp.Blazor.Pages.Services.Models;
using ClientsWebApp.Domain.Services;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Services
{
    public partial class CreateService
    {
        [Parameter] public Guid SpecializationId { get; set; }
        [Parameter] public EventCallback<CreateServiceModel> OnModelCreated { get; set; }
        private CreateServiceData Data { get; set; } = new CreateServiceData();
        private List<string> Categories = new List<string>() { "consultation", "analyses", "diagnostics" };

        protected override void OnInitialized()
        {
            Data = new CreateServiceData() { CategoryName=Categories.First()};
        }
        private async Task CreateAsync()
        {
            var createModel = new CreateServiceModel(Data.Name, Data.Price, Data.Status, SpecializationId, Data.CategoryName);
            await OnModelCreated.InvokeAsync(createModel);
            Data = new CreateServiceData();
        }
    }
}
