using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Identity;
using ClientsWebApp.Blazor.Components;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Identity
{
    public partial class Register : CancellableComponent
    {
        [Inject] public IIdentityManager IdentityManager { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        private RegisterData Data;
        protected FormSubmitButton SubmitButton { get; set; }
        private string ErrorMessage;
        protected override void OnInitialized()
        {
            Data = new RegisterData();
            ErrorMessage = "";
        }

        private async Task RegisterAsync()
        {
            SubmitButton.StartLoading();
            try
            {
                await IdentityManager.SingUpAsync(Data, _cts.Token);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            finally
            {
                SubmitButton.StopLoading();
            }

            Navigation.NavigateTo("/login");
        }
    }
}
