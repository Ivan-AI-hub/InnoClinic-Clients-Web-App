using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Pages.Results.Models;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Results
{

    [Authorize(Roles = "Doctor")]
    public partial class CreateResult : CancellableComponent
    {
        [Parameter] public Guid AppointmentId { get; set; }
        [Inject] public IAppointmentService AppointmentService { get; set; }
        [Inject] public IResultService ResultService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private CreateResultData Data { get; set; } = new CreateResultData();
        private Appointment Appointment { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var filtration = new AppointmentFiltrationModel() { Status = true };
            //Create a getById method in the back end
            Appointment = (await AppointmentService.GetPageAsync(new Domain.Page(100, 1), filtration, _cts.Token)).First(x => x.Id == AppointmentId);
            Data = new CreateResultData() { AppointmentId = Appointment.Id, DoctorId = Appointment.Doctor.Id, PatientId = Appointment.Patient.Id };
            this.StateHasChanged();
        }
        private async Task CreateAsync()
        {
            SubmitButton?.StartLoading();

            var createModel = new CreateResultModel(Data.Complaints, Data.Conclusion, Data.Recomendations, Data.AppointmentId, Data.PatientId, Data.DoctorId);
            try
            {
                await ResultService.CreateAsync(createModel, _cts.Token);
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