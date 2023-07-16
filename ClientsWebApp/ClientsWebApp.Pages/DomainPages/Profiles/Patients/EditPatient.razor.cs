using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Profiles.Patients
{
    [Authorize(Roles = "Patient")]
    public partial class EditPatient : CancellableComponent
    {
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IPatientService PatientService { get; set; }
        [Inject] public IImageService ImageService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private EditPatientData Data { get; set; }
        private string ErrorMessage;
        private Patient OldPatient;
        private byte[] OldPicture;
        protected override async Task OnInitializedAsync()
        {
            var email = await authStateHelper.GetEmailAsync();
            OldPatient = await PatientService.GetByEmailAsync(email, _cts.Token);
            Data = new EditPatientData(OldPatient);

            if (OldPatient.Info.Photo != null)
            {
                OldPicture = (await ImageService.GetAsync(OldPatient.Info.Photo.Name, _cts.Token)).Content;
            }
        }

        private async Task EditAsync()
        {
            SubmitButton?.StartLoading();

            var imageName = Data.Picture == null ? OldPatient.Info.Photo : new ImageName(Data.Picture.FileName);
            var update = new UpdatePatientModel(imageName, Data.FirstName, Data.LastName, Data.MiddleName, Data.BirthDay, Data.PhoneNumber);
            try
            {
                await PatientService.UpdateAsync(OldPatient.Id, update, _cts.Token);
                if (Data.Picture != null && Data.Picture.FileName != OldPatient.Info.Photo.Name)
                {
                    await ImageService.DeleteAsync(OldPatient.Info.Photo.Name, _cts.Token);
                    await ImageService.CreateAsync(Data.Picture, _cts.Token);
                }
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
