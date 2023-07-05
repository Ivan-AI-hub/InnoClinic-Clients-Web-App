using ClientsWebApp.Domain;
using Microsoft.AspNetCore.Components;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Specializations;
using Microsoft.AspNetCore.Authorization;

namespace ClientsWebApp.Blazor.Pages.Specializations
{
    [Authorize(Roles = "Admin")]
    public partial class SpecializationsList : CancellableComponent
    {
        [Inject] public ISpecializationService SpecializationService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        private Page Page { get; set; }
        private PageStatus Status => Page.GetPageStatus(Specializations == null ? 0 : Specializations.Count());
        private IEnumerable<Specialization>? Specializations { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            await SpecializationsUpdateAsync();
            this.StateHasChanged();
        }

        private async Task SpecializationsUpdateAsync()
        {
            Specializations = null;
            this.StateHasChanged();
            Specializations = await SpecializationService.GetInfoAsync(Page, _cts.Token);
            this.StateHasChanged();
        }

        protected async Task SetPreviousPage()
        {
            Page.Number--;
            await SpecializationsUpdateAsync();
        }
        protected async Task SetNextPage()
        {
            Page.Number++;
            await SpecializationsUpdateAsync();
        }

        private void ToCreatePage()
        {
            NavigationManager.NavigateTo("/specializations/create");
        }
    }
}
