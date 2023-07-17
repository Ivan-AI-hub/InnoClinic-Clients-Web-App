using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Offices;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Offices
{
    [Authorize(Roles = "Admin")]
    public partial class EditOffice : CancellableComponent
    {
        [Parameter] public Guid OfficeId { get; set; }
        [Inject] public IOfficeManager OfficeManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private EditOfficeData Data { get; set; }
        private string ErrorMessage;
        private OfficeDTO OldOffice;
        protected override async Task OnInitializedAsync()
        {
            OldOffice = await OfficeManager.GetByIdAsync(OfficeId, _cts.Token);
            Data = new EditOfficeData(OldOffice);
            StateHasChanged();
        }

        private async Task EditAsync()
        {
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
