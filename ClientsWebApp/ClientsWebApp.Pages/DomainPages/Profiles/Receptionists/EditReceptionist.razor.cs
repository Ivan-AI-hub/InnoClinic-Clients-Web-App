using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Receptionists;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles.Receptionist;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Profiles.Receptionists
{
    [Authorize(Roles = "Admin")]
    public partial class EditReceptionist : CancellableComponent
    {
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IReceptionistManager ReceptionistManager { get; set; }
        [Inject] public IOfficeManager OfficeManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private IEnumerable<Office> Offices { get; set; }
        private EditReceptionistData Data { get; set; }
        private string ErrorMessage;
        private ReceptionistDTO OldReceptionist;
        protected override async Task OnInitializedAsync()
        {
            var page = new Page(100, 1);
            var email = await authStateHelper.GetEmailAsync();
            OldReceptionist = await ReceptionistManager.GetByEmailAsync(email, _cts.Token);
            Data = new EditReceptionistData(OldReceptionist);

            Offices = await OfficeManager.GetInfoPageAsync(page, _cts.Token);

            if (OldReceptionist.Office == null)
            {
                Data.OfficeId = Offices.Select(x => x.Id).First();
            }
        }

        private async Task EditAsync()
        {
            SubmitButton?.StartLoading();

            try
            {
                await ReceptionistManager.EditAsync(OldReceptionist, Data, _cts.Token);
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
            Cancel();
        }
        private void Cancel()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
