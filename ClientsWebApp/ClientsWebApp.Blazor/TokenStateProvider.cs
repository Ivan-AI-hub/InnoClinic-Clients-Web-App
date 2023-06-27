using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Tokens;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ClientsWebApp.Blazor
{
    internal class TokenStateProvider : AuthenticationStateProvider
    {
        private IStorageService _storageService;
        public TokenStateProvider(IStorageService storageService)
        {
            _storageService = storageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _storageService.GetAsync<SecurityToken>(nameof(SecurityToken));
            if (token == null)
            {
                return GetAnonymous();
            }

            if (String.IsNullOrEmpty(token.AccessToken) || token.ExpireAt < DateTime.UtcNow)
            {
                return GetAnonymous();
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, token.Email),
                new Claim(ClaimTypes.Role, token.Role.ToString()),
                new Claim(ClaimTypes.Expired, token.ExpireAt.ToLongDateString()),
            };

            var identity = new ClaimsIdentity(claims, "Bearer");
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationState(principal);
        }

        public void MakeUserAnonymous()
        {
            _storageService.RemoveAsync(nameof(SecurityToken));

            var authState = Task.FromResult(GetAnonymous());
            NotifyAuthenticationStateChanged(authState);
        }

        private AuthenticationState GetAnonymous()
        {
            var anonymousIdentity = new ClaimsIdentity();
            var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
            return new AuthenticationState(anonymousPrincipal);
        }
    }
}
