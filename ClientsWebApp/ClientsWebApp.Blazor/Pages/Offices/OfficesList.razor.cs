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
        [Inject] public IOfficeService OfficeService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private Page Page { get; set; }
        private IEnumerable<Office>? Offices { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            await OfficesUpdateAsync();
            this.StateHasChanged();
        }

        private async Task OfficesUpdateAsync()
        {
            Offices = null;
            this.StateHasChanged();
            Offices = await OfficeService.GetPageAsync(Page, _cts.Token);
            this.StateHasChanged();
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
        private void ToCreatePage()
        {
            NavigationManager.NavigateTo("/offices/create");
        }
    }
}
