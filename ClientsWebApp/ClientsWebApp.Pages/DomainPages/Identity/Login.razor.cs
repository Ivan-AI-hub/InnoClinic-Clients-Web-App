using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Identity;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Domain.Identity.Tokens;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace ClientsWebApp.Pages.DomainPages.Identity
{
    public partial class Login : CancellableComponent
    {
        [Inject] public IIdentityManager identityManager { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        protected LoginData LoginData { get; set; }
        protected FormSubmitButton SubmitButton { get; set; }
        protected string ErrorMessage { get; set; }

        protected override void OnInitialized()
        {
            LoginData = new LoginData();
            ErrorMessage = "";
        }

        protected async Task LoginAsync()
        {
            SubmitButton.StartLoading();
            try
            {
                await identityManager.SingInAsync(LoginData, _cts.Token);
                Navigation.NavigateTo("/", true);
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
        }
    }
}
