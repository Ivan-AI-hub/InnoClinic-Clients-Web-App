using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ClientsWebApp.Blazor
{
    public class AuthenticationStateHelper
    {
        private AuthenticationStateProvider _stateProvider;

        public AuthenticationStateHelper(AuthenticationStateProvider stateProvider)
        {
            _stateProvider = stateProvider;
        }

        public async Task<string> GetEmailAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            return state.User.FindFirst(ClaimTypes.Email)?.Value ?? throw new Exception("Token auth failed");
        }

        public async Task<string> GetRoleAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            return state.User.FindFirst(ClaimTypes.Role)?.Value ?? throw new Exception("Token auth failed");
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            var state = await _stateProvider.GetAuthenticationStateAsync();
            return state.User.Claims.Any();
        }
    }
}
