using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Receptionists;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Profiles.Receptionist;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Receptionists
{
    public partial class ReceptionistsList : CancellableComponent
    {
        [Inject] public IReceptionistManager ReceptionistManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private Page Page { get; set; }
        private ReceptionistFiltrationModel FiltrationModel { get; set; }
        private IEnumerable<ReceptionistDTO>? Receptionists { get; set; }
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
            Receptionists = await ReceptionistManager.GetPageAsync(Page, FiltrationModel, _cts.Token);
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
