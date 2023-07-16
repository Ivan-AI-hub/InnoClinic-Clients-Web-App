using ClientsWebApp.Application.Models.Identity;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Domain.Identity.Tokens;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Components;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClientsWebApp.Pages.DomainPages.Identity
{
    public partial class Login : CancellableComponent
    {
        [Inject] public IStorageService LocalStorageService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public IAuthorizationService Service { get; set; }

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
                var accessToken = await Service.SingIn(LoginData.Email, LoginData.Password, _cts.Token);
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(accessToken.Token);

                var token = new SecurityToken(accessToken.Token, LoginData.Email, Enum.Parse<Role>(jwt.Claims.First(x => x.Type == ClaimTypes.Role).Value), DateTime.UtcNow.AddMinutes(60));

                await LocalStorageService.SetAsync(nameof(SecurityToken), token);
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
