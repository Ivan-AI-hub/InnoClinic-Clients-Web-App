using ClientsWebApp.Domain.Results;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.Pages.Results
{
    [Authorize(Roles = "Doctor")]
    public partial class EditResult : CancellableComponent
    {
        [Parameter] public Guid AppointmentId { get; set; }
        [Inject] public IResultService ResultService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }

        private EditResultData Data { get; set; }
        private AppointmentResult Result { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Result = await ResultService.GetForAppointmentAsync(AppointmentId, _cts.Token);
            Data = new EditResultData(Result);
        }
        private async Task EditAsync()
        {
            SubmitButton?.StartLoading();

            var editModel = new UpdateResultModel(Data.Complaints, Data.Conclusion, Data.Recomendations);
            try
            {
                await ResultService.UpdateAsync(Result.Id, editModel, _cts.Token);
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