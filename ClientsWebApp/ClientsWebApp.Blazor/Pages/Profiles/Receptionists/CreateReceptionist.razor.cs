using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Infrastructure;
using ClientsWebApp.Blazor.Pages.Profiles.Receptionists.Models;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles;
using ClientsWebApp.Domain.Profiles.Receptionist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Receptionists
{
    [Authorize(Roles = "Admin")]
    public partial class CreateReceptionist : CancellableComponent
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IReceptionistService ReceptionistService { get; set; }
        [Inject] public IOfficeService OfficeService { get; set; }
        [Inject] public IImageService ImageService { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private CreateReceptionistData Data { get; set; } = new CreateReceptionistData();
        private IEnumerable<Office> Offices { get; set; } = new List<Office>();
        private string ErrorMessage = "";
        private string Email = "";
        protected override async Task OnInitializedAsync()
        {
            var page = new Page(100, 1);
            Offices = await OfficeService.GetPageAsync(page, _cts.Token);
            Email = await authStateHelper.GetEmailAsync();
            this.StateHasChanged();
        }

        private async Task CreateAsync()
        {
            SubmitButton?.StartLoading();

            var info = new CreateHumanInfo(new ImageName(Data.Picture.FileName), Email, Data.FirstName, Data.LastName, Data.MiddleName, Data.BirthDay);
            var createModel = new CreateReceptionistModel(info, new OfficeId(Data.OfficeId));
            try
            {
                var user = await ReceptionistService.CreateAsync(createModel, _cts.Token);
                await ImageService.CreateAsync(Data.Picture, _cts.Token);
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
