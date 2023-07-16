using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain.Specializations;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Profiles.Doctors
{
    [Authorize(Roles = "Doctor")]
    public partial class EditDoctor : CancellableComponent
    {
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IDoctorService DoctorService { get; set; }
        [Inject] public ISpecializationService SpecializationService { get; set; }
        [Inject] public IOfficeService OfficeService { get; set; }
        [Inject] public IImageService ImageService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private Application.Models.Doctors.EditDoctorData Data { get; set; }
        private IEnumerable<Specialization> Specializations { get; set; }
        private IEnumerable<Office> Offices { get; set; }

        private byte[]? OldPicture;
        private Doctor OldDoctor;
        private string ErrorMessage;
        private string Email;
        protected override async Task OnInitializedAsync()
        {
            var page = new Page(100, 1);
            Email = await authStateHelper.GetEmailAsync();
            OldDoctor = await DoctorService.GetByEmailAsync(Email, _cts.Token);
            Data = new EditDoctorData(OldDoctor);
            Specializations = await SpecializationService.GetInfoAsync(page, _cts.Token);
            Offices = await OfficeService.GetPageAsync(page, _cts.Token);

            if (OldDoctor.Specialization == null)
            {
                Data.Specialization = Specializations.Select(x => x.Name).First();
            }
            if (OldDoctor.Office == null)
            {
                Data.OfficeId = Offices.Select(x => x.Id).First();
            }
            if (OldDoctor.Info.Photo != null)
            {
                OldPicture = (await ImageService.GetAsync(OldDoctor.Info.Photo.Name, _cts.Token)).Content;
            }
        }

        private async Task EditAsync()
        {
            SubmitButton?.StartLoading();

            var imageName = Data.Picture == null ? OldDoctor.Info.Photo : new ImageName(Data.Picture.FileName);
            var updateModel = new UpdateDoctorModel(imageName, Data.FirstName, Data.LastName, Data.MiddleName, Data.BirthDay, Data.Specialization, Data.OfficeId, Data.CareerStartYear, Data.Status);
            try
            {
                await DoctorService.UpdateAsync(OldDoctor.Id, updateModel, _cts.Token);
                if (Data.Picture != null && Data.Picture.FileName != OldDoctor.Info.Photo?.Name)
                {
                    await ImageService.DeleteAsync(OldDoctor.Info.Photo.Name, _cts.Token);
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
