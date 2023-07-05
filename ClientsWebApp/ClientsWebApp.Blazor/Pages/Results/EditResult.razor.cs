using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Pages.Results.Models;
using ClientsWebApp.Blazor.Pages.Services.Models;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Results;
using ClientsWebApp.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Results
{
[Authorize(Roles ="Doctor")]
public partial class EditResult : CancellableComponent
{
    [Parameter] public Guid AppointmentId { get; set; }
    [Inject] public IResultService ResultService { get; set; }
    private EditResultData Data { get; set; }
    private AppointmentResult Result { get; set; } 
    protected override async Task OnInitializedAsync()
    {
        Result = await ResultService.GetForAppointmentAsync(AppointmentId, _cts.Token);
        Data = new EditResultData(Result);
    }
    private async Task EditAsync()
    {
        var editModel = new UpdateResultModel(Data.Complaints, Data.Conclusion, Data.Recomendations);
        await ResultService.UpdateAsync(Result.Id, editModel, _cts.Token);
    }
}
}