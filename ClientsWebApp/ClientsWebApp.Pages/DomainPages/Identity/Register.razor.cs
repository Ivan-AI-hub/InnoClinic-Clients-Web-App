using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Identity;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Identity
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
