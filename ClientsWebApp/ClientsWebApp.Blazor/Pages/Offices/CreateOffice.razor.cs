using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Offices;
using ClientsWebApp.Blazor.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Offices
{
    [Authorize(Roles = "Admin")]
    public partial class CreateOffice : CancellableComponent
    {
        [Inject] public IOfficeManager OfficeManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private CreateOfficeData Data { get; set; }
        private string ErrorMessage;
        protected override void OnInitialized()
        {
            Data = new CreateOfficeData();
        }

        private async Task CreateAsync()
        {
            SubmitButton?.StartLoading();

            try
            {
                OfficeManager.CreateAsync(Data, _cts.Token);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            finally
            {
                SubmitButton?.StopLoading();
            }
            Cancel();
        }
        private void Cancel()
        {
            NavigationManager.NavigateTo($"/offices");
        }
    }
}

