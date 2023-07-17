using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Receptionists;
using ClientsWebApp.Blazor;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Offices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Receptionists
{
    [Authorize(Roles = "Admin")]
    public partial class CreateReceptionist : CancellableComponent
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IReceptionistManager ReceptionistManager { get; set; }
        [Inject] public IOfficeManager OfficeManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private CreateReceptionistData Data { get; set; } = new CreateReceptionistData();
        private IEnumerable<Office> Offices { get; set; } = new List<Office>();
        private string ErrorMessage = "";
        protected override async Task OnInitializedAsync()
        {
            var page = new Page(100, 1);
            Offices = await OfficeManager.GetInfoPageAsync(page, _cts.Token);
            Data.OfficeId = Offices.FirstOrDefault()?.Id ?? default;
            Data.Email = await authStateHelper.GetEmailAsync();
            StateHasChanged();
        }

        private async Task CreateAsync()
        {
            SubmitButton?.StartLoading();
            try
            {
                await ReceptionistManager.CreateAsync(Data, _cts.Token);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            finally
            {
                SubmitButton?.StopLoading();
            }
            NavigationManager.NavigateTo("/");
        }
    }
}
