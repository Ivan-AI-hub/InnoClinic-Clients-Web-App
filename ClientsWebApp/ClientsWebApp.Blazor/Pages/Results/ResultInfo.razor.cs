using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Domain.Exceptions;
using ClientsWebApp.Domain.Results;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Results
{
    public partial class ResultInfo
    {
        [Parameter] public Guid AppointmentId { get; set; }
        [Parameter] public EventCallback<AppointmentResult> OnResultAvailable { get; set; }
        [CascadingParameter] public IModalService Modal { get; set; }
        private AppointmentResult result;
        private bool IsNotFound = false;
        protected async override Task OnInitializedAsync()
        {
            try
            {
                result = await ResultManager.GetForAppointmentAsync(AppointmentId, _cts.Token);
                await OnResultAvailable.InvokeAsync(result);
            }
            catch (NotFoundException)
            {
                IsNotFound = true;
            }
        }

        private void NavigateToEditPage()
        {
            var parameters = new ModalParameters()
                            .Add(nameof(EditResult.AppointmentId), AppointmentId)
                            .Add(nameof(EditResult.Result), result);
            Modal.Show<EditResult>("Edit result", parameters);
        }

        private void NavigateToCreateResultPage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large
            };
            var parameters = new ModalParameters().Add(nameof(CreateResult.AppointmentId), AppointmentId);
            Modal.Show<CreateResult>("Create result", parameters, options);
        }


    }
}