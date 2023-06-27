using ClientsWebApp.Domain.Tokens;

namespace ClientsWebApp.Domain
{
    public interface IAuthorizationService
    {
        public Task<User> SingUpAsync(SingUpModel model, CancellationToken cancellationToken = default);
        public Task ConfirmEmailAsync(Guid userId, CancellationToken cancellationToken = default);
        public Task<AccessToken> SingIn(string email, string password, CancellationToken cancellationToken = default);
        public Task UpdateRole(string email, Role role, CancellationToken cancellationToken = default);
    }
}
