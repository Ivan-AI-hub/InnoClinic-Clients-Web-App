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
    public partial class CreateOffice : CancellableComponent
    {
        [Inject] public IOfficeService OfficeService { get; set; }
        [Inject] public IImageService ImageService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private CreateOfficeData Data { get; set; }
        private string ErrorMessage;
        protected override void OnInitialized()
        {
            Data = new CreateOfficeData();
        }

        private async Task CreateAsync()
        {
            SubmitButton?.StartLoading();
            var createModel = new CreateOfficeModel(new ImageName(Data.Picture.FileName), Data.City, Data.Street, Data.HouseNumber, Data.OfficeNumber, Data.PhoneNumber, Data.Status);
            try
            {
                var user = await OfficeService.CreateAsync(createModel, _cts.Token);
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
            Cancel();
        }
        private void Cancel()
        {
            NavigationManager.NavigateTo($"/offices");
        }
    }
}

