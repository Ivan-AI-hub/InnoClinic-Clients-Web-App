using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Profiles.Receptionist;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Profiles.Receptionists
{
    public partial class ReceptionistsList : CancellableComponent
    {
        [Inject] public IReceptionistService ReceptionistService { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private Page Page { get; set; }
        private ReceptionistFiltrationModel FiltrationModel { get; set; }
        private IEnumerable<Receptionist>? Receptionists { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            FiltrationModel = new ReceptionistFiltrationModel();
            await ReceptionistsUpdateAsync();
            StateHasChanged();
        }

        private async Task ReceptionistsUpdateAsync()
        {
            SubmitButton?.StartLoading();

            Receptionists = null;
            StateHasChanged();
            Receptionists = await ReceptionistService.GetPageAsync(Page, FiltrationModel, _cts.Token);
            StateHasChanged();

            SubmitButton?.StopLoading();
        }

        protected async Task SetPreviousPage()
        {
            Page.Number--;
            await ReceptionistsUpdateAsync();
        }
        protected async Task SetNextPage()
        {
            Page.Number++;
            await ReceptionistsUpdateAsync();
        }
        protected PageStatus GetPageStatus()
        {
            return Page.GetPageStatus(Receptionists == null ? 0 : Receptionists.Count());
        }
    }
}
