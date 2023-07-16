using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Offices
{
    [Authorize(Roles = "Admin")]
    public partial class OfficesList : CancellableComponent
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Inject] public IOfficeService OfficeService { get; set; }

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
            Offices = await OfficeService.GetPageAsync(Page, _cts.Token);
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
        private void ToCreatePage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large
            };
            Modal.Show<CreateOffice>("Create", options);
        }
    }
}
