using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Offices;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Offices
{
    [Authorize(Roles = "Admin")]
    public partial class EditOffice : CancellableComponent
    {
        [Parameter] public Guid OfficeId { get; set; }
        [Parameter] public OfficeDTO? OldOffice { get; set; }
        [Inject] public IOfficeManager OfficeManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private EditOfficeData Data { get; set; }
        private string ErrorMessage;
        private Image? _image;
        protected override async Task OnInitializedAsync()
        {
            OldOffice ??= await OfficeManager.GetByIdAsync(OfficeId, _cts.Token);
            Data = new EditOfficeData(OldOffice);
            if (OldOffice.Photo is not null)
            {
                _image = await OldOffice.Photo;
            }
        }

        private async Task EditAsync()
        {
            if (OldOffice is null)
            {
                return;
            }

            SubmitButton?.StartLoading();
            try
            {
                await OfficeManager.EditAsync(OldOffice, Data, _cts.Token);
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
            NavigationManager.NavigateTo($"/offices/{OfficeId}");
        }
    }
}
