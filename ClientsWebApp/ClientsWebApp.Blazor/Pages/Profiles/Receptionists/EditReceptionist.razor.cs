using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Infrastructure;
using ClientsWebApp.Blazor.Pages.Profiles.Receptionists.Models;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles.Receptionist;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Receptionists
{
    [Authorize(Roles = "Admin")]
    public partial class EditReceptionist : CancellableComponent
    {
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IReceptionistService ReceptionistService { get; set; }
        [Inject] public IOfficeService OfficeService { get; set; }
        [Inject] public IImageService ImageService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private IEnumerable<Office> Offices { get; set; }
        private EditReceptionistData Data { get; set; }
        private string ErrorMessage;
        private Receptionist OldReceptionist;
        private byte[] OldPicture;
        protected override async Task OnInitializedAsync()
        {
            var page = new Page(100, 1);
            var email = await authStateHelper.GetEmailAsync();
            OldReceptionist = await ReceptionistService.GetByEmailAsync(email, _cts.Token);
            Data = new EditReceptionistData(OldReceptionist);

            Offices = await OfficeService.GetPageAsync(page, _cts.Token);

            if (OldReceptionist.Office == null)
            {
                Data.OfficeId = Offices.Select(x => x.Id).First();
            }
            if (OldReceptionist.Info.Photo != null)
            {
                OldPicture = (await ImageService.GetAsync(OldReceptionist.Info.Photo.Name, _cts.Token)).Content;
            }
        }

        private async Task EditAsync()
        {
            SubmitButton?.StartLoading();

            var imageName = Data.Picture == null ? OldReceptionist.Info.Photo : new ImageName(Data.Picture.FileName);
            var update = new UpdateReceptionistModel(imageName, Data.FirstName, Data.LastName, Data.MiddleName, Data.BirthDay, Data.OfficeId);
            try
            {
                await ReceptionistService.UpdateAsync(OldReceptionist.Id, update, _cts.Token);
                if (Data.Picture != null && Data.Picture.FileName != OldReceptionist.Info.Photo.Name)
                {
                    await ImageService.DeleteAsync(OldReceptionist.Info.Photo.Name, _cts.Token);
                    await ImageService.CreateAsync(Data.Picture, _cts.Token);
                }
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
