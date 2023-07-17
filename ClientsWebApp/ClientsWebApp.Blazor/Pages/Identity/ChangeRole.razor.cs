using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Identity;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Identity
{
    public partial class ChangeRole
    {
        [Inject] IIdentityManager IdentityManager { get; set; }

        [Parameter] public string Email { get; set; }
        [Parameter] public EventCallback OnRoleChanged { get; set; }

        private bool IsRoleChanges { get; set; }
        private bool IsLoading { get; set; } = false;
        private List<string> Roles { get; set; } = new List<string>()
        {
            "Patient",
            "Admin",
            "Doctor"
        };
        private ChangeRoleData ChangeRoleData { get; set; }

        protected override void OnInitialized()
        {
            ChangeRoleData = new ChangeRoleData()
            {
                Role = Roles.First()
            };
        }

        private void StartChanging()
        {
            IsRoleChanges = true;
        }

        private async Task ChangeAsync()
        {
            IsLoading = true;

            await IdentityManager.ChangeRoleAsync(Email, ChangeRoleData, _cts.Token);
            await OnRoleChanged.InvokeAsync();

            IsRoleChanges = false;

            IsLoading = false;
        }
    }
}