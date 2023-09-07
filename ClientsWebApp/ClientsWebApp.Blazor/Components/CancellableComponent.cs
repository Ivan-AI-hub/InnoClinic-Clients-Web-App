using ClientsWebApp.Blazor.AppLocalization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace ClientsWebApp.Blazor.Components
{
    public class CancellableComponent : ComponentBase, IDisposable
    {
        [Inject] protected IStringLocalizer<Localization> _localizer { get; set; }

        protected CancellationTokenSource _cts = new();
        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}
