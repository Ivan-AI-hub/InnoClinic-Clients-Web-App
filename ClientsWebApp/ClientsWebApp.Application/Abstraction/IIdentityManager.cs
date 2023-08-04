using ClientsWebApp.Application.Models.Identity;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IIdentityManager
    {
        public Task SingInAsync(LoginData data, CancellationToken cancellationToken);
        public Task SingUpAsync(RegisterData data, CancellationToken cancellationToken);
        public Task ChangeRoleAsync(string email, ChangeRoleData data, CancellationToken cancellationToken);
        public Task ConfirmEmailAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
