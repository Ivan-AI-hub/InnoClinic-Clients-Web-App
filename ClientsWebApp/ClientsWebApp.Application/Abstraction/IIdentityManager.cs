using ClientsWebApp.Application.Models.Identity;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IIdentityManager
    {
        public Task<ManagerResult> SingInAsync(LoginData data, CancellationToken cancellationToken);
        public Task<ManagerResult> SingUpAsync(RegisterData data, CancellationToken cancellationToken);
        public Task<ManagerResult> ChangeRoleAsync(ChangeRoleData data, CancellationToken cancellationToken);
        public Task<ManagerResult> ConfirmEmailAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
