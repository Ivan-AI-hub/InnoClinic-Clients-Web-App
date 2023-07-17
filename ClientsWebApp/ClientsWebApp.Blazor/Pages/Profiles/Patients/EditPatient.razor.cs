using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Blazor;
using ClientsWebApp.Blazor.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Patients
{
    [Authorize(Roles = "Patient")]
    public partial class EditPatient : CancellableComponent
    {
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IPatientManager PatientManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private EditPatientData Data { get; set; }
        private string ErrorMessage;
        private PatientDTO OldPatient;
        protected override async Task OnInitializedAsync()
        {
            var email = await authStateHelper.GetEmailAsync();
            OldPatient = await PatientManager.GetByEmailAsync(email, _cts.Token);
            Data = new EditPatientData(OldPatient);
        }

        private async Task EditAsync()
        {
            SubmitButton?.StartLoading();

            try
            {
                await PatientManager.EditAsync(OldPatient, Data, _cts.Token);
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
            NavigationManager.NavigateTo("/");
        }
    }
}
