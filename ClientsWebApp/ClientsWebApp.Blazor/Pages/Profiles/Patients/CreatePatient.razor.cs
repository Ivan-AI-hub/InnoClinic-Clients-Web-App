using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Blazor.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Patients
{
    [Authorize(Roles = "Patient")]
    public partial class CreatePatient : CancellableComponent
    {
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IPatientManager PatientManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private CreatePatientData Data { get; set; }
        protected FormSubmitButton SubmitButton { get; set; }

        private string ErrorMessage;
        protected override async Task OnInitializedAsync()
        {
            Data = new CreatePatientData();
            Data.Email = await authStateHelper.GetEmailAsync();
        }

        private async Task CreateAsync()
        {
            SubmitButton.StartLoading();
            try
            {
                await PatientManager.CreateAsync(Data, _cts.Token);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            finally
            {
                SubmitButton.StopLoading();
            }
            NavigationManager.NavigateTo("/");
        }
    }
}
