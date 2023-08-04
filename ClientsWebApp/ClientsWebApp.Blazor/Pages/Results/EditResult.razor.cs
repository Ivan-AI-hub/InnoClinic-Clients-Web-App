using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Results;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Results
{
    [Authorize(Roles = "Doctor")]
    public partial class EditResult : CancellableComponent
    {
        [Parameter] public AppointmentResult Result { get; set; }
        [Parameter] public Guid AppointmentId { get; set; }
        [Inject] public IResultManager ResultManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }

        private EditResultData Data { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Result = Result ?? await ResultManager.GetForAppointmentAsync(AppointmentId, _cts.Token);
            Data = new EditResultData(Result);
        }
        private async Task EditAsync()
        {
            SubmitButton?.StartLoading();
            try
            {
                await ResultManager.EditAsync(Result.Id, Data, _cts.Token);
            }
            finally
            {
                SubmitButton?.StopLoading();
            }
            Cancel();
        }
        private void Cancel()
        {
            NavigationManager.NavigateTo($"/appointments/{AppointmentId}");
        }
    }
}