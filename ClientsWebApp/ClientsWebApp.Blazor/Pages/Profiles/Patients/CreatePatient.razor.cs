using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Infrastructure;
using ClientsWebApp.Blazor.Pages.Profiles.Patients.Models;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Profiles;
using ClientsWebApp.Domain.Profiles.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Patients
{
    [Authorize(Roles = "Patient")]
    public partial class CreatePatient: CancellableComponent
    {
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IPatientService PatientService { get; set; }
        [Inject] public IImageService ImageService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private CreatePatientData Data { get; set; }

        private string ErrorMessage;
        private string Email;
        protected override async Task OnInitializedAsync()
        {
            Data = new CreatePatientData();
            Email = await authStateHelper.GetEmailAsync();
        }
        
        private async Task CreateAsync()
        {
            var info = new CreateHumanInfo(new ImageName(Data.Picture.FileName), Email, Data.FirstName, Data.LastName, Data.MiddleName, Data.BirthDay);
            var createModel = new CreatePatientModel(info, Data.PhoneNumber);
            try
            {
                var user = await PatientService.CreateAsync(createModel, _cts.Token);
                await ImageService.CreateAsync(Data.Picture, _cts.Token);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            NavigationManager.NavigateTo("/");
        }
    }
}
