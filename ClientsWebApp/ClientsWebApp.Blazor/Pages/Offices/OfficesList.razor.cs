using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Offices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Offices
{
    [Authorize(Roles = "Admin")]
    public partial class OfficesList : CancellableComponent
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Inject] public IOfficeManager OfficeManager { get; set; }

        private Page Page { get; set; }
        private IEnumerable<Office>? Offices { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            await OfficesUpdateAsync();
            StateHasChanged();
        }

        private async Task OfficesUpdateAsync()
        {
            Offices = null;
            StateHasChanged();
            Offices = await OfficeManager.GetInfoPageAsync(Page, _cts.Token);
            StateHasChanged();
        }

        protected async Task SetPreviousPage()
        {
            Page.Number--;
            await OfficesUpdateAsync();
        }
        protected async Task SetNextPage()
        {
            Page.Number++;
            await OfficesUpdateAsync();
        }
        protected PageStatus GetPageStatus()
        {
            return Page.GetPageStatus(Offices == null ? 0 : Offices.Count());
        }
        private async Task ToCreatePage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large
            };
            await Modal.Show<CreateOffice>("Create", options).Result;
            await OfficesUpdateAsync();
        }
    }
}
