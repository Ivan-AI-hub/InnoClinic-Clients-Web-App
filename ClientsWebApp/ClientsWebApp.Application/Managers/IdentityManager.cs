using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Identity;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Domain.Identity.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ClientsWebApp.Application.Managers
{
    public class IdentityManager : IIdentityManager
    {
        private readonly IStorageService _localStorageService;
        private readonly IAuthorizationService _authorizationService;

        public IdentityManager(IStorageService localStorageService, IAuthorizationService authorizationService)
        {

            _localStorageService = localStorageService;
            _authorizationService = authorizationService;
        }

        public async Task ChangeRoleAsync(string email, ChangeRoleData data, CancellationToken cancellationToken)
        {
            var role = Enum.Parse<Role>(data.Role);
            await _authorizationService.UpdateRole(email, role, cancellationToken);
        }

        public async Task ConfirmEmailAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            await _authorizationService.ConfirmEmailAsync(userId, cancellationToken);
        }

        public async Task SingInAsync(LoginData data, CancellationToken cancellationToken)
        {
            var accessToken = await _authorizationService.SingIn(data.Email, data.Password, cancellationToken);

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(accessToken.Token);
            var token = new SecurityToken(accessToken.Token, data.Email, Enum.Parse<Role>(jwt.Claims.First(x => x.Type == ClaimTypes.Role).Value), DateTime.UtcNow.AddMinutes(60));
            await _localStorageService.SetAsync(nameof(SecurityToken), token);
        }

        public async Task SingUpAsync(RegisterData data, CancellationToken cancellationToken)
        {
            var model = new SingUpModel(data.Email, data.Password, data.RePassword);
            var user = await _authorizationService.SingUpAsync(model, cancellationToken);
            //dd
            await ConfirmEmailAsync(user.Id, cancellationToken);
        }
    }
}
