using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ClientsWebApp.Pages
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
    }
}
