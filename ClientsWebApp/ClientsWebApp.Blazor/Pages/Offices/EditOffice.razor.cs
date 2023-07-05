using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Pages.Offices.Models;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Offices
{
    [Authorize(Roles = "Admin")]
    public partial class EditOffice : CancellableComponent
    {
        [Parameter] public Guid OfficeId { get; set; }
        [Inject] public IOfficeService OfficeService { get; set; }
        [Inject] public IImageService ImageService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private EditOfficeData Data { get; set; }
        private string ErrorMessage;
        private Office OldOffice;
        private byte[] OldPicture;
        protected override async Task OnInitializedAsync()
        {
            OldOffice = await OfficeService.GetByIdAsync(OfficeId, _cts.Token);
            Data = new EditOfficeData(OldOffice);
            if (OldOffice.Photo != null)
            {
                OldPicture = (await ImageService.GetAsync(OldOffice.Photo.Name, _cts.Token)).Content;
            }
            this.StateHasChanged();
        }

        private async Task EditAsync()
        {
            var imageName = Data.Picture == null ? OldOffice.Photo : new ImageName(Data.Picture.FileName);
            var update = new UpdateOfficeModel(imageName, Data.City, Data.Street, Data.HouseNumber, Data.OfficeNumber, Data.PhoneNumber, Data.Status);
            try
            {
                await OfficeService.UpdateAsync(OldOffice.Id, update, _cts.Token);
                if (Data.Picture != null && Data.Picture.FileName != OldOffice.Photo.Name)
                {
                    await ImageService.DeleteAsync(OldOffice.Photo.Name, _cts.Token);
                    await ImageService.CreateAsync(Data.Picture, _cts.Token);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            Cancel();
        }
        private void Cancel()
        {
            NavigationManager.NavigateTo($"/offices/{OfficeId}");
        }
    }
}
