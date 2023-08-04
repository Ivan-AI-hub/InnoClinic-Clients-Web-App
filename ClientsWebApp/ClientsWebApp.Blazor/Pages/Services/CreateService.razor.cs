using ClientsWebApp.Application.Models.Services;
using ClientsWebApp.Blazor.Components;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Services
{
    public partial class CreateService
    {
        [Parameter] public Guid SpecializationId { get; set; }
        [Parameter] public EventCallback<CreateServiceData> OnModelCreated { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private CreateServiceData Data { get; set; } = new CreateServiceData();
        private List<string> Categories = new List<string>() { "consultation", "analyses", "diagnostics" };

        protected override void OnInitialized()
        {
            Data = new CreateServiceData() { CategoryName = Categories.First(), SpecializationId = SpecializationId };
        }
        private async Task CreateAsync()
        {
            SubmitButton?.StartLoading();

            await OnModelCreated.InvokeAsync(Data);
            Data = new CreateServiceData();

            SubmitButton?.StopLoading();
        }
    }
}
