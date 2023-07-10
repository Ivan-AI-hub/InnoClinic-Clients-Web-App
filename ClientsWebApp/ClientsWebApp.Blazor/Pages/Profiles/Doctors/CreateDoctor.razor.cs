using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Infrastructure;
using ClientsWebApp.Blazor.Pages.Profiles.Doctors.Models;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain.Specializations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Doctors
{
    [Authorize(Roles = "Doctor")]
    public partial class CreateDoctor : CancellableComponent
    {
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IDoctorService DoctorService { get; set; }
        [Inject] public ISpecializationService SpecializationService { get; set; }
        [Inject] public IOfficeService OfficeService { get; set; }
        [Inject] public IImageService ImageService { get; set; }
        private CreateDoctorData Data { get; set; }
        protected FormSubmitButton SubmitButton { get; set; }
        private IEnumerable<Specialization> Specializations { get; set; }
        private IEnumerable<Office> Offices { get; set; }
        private string ErrorMessage;
        private string Email;
        protected override async Task OnInitializedAsync()
        {
            var page = new Page(100, 1);
            Data = new CreateDoctorData();
            Email = await authStateHelper.GetEmailAsync();
            Specializations = await SpecializationService.GetInfoAsync(page, _cts.Token);
            Offices = await OfficeService.GetPageAsync(page, _cts.Token);
        }

        private async Task CreateAsync()
        {
            SubmitButton.StartLoading();
            var info = new CreateHumanInfo(new ImageName(Data.Picture.FileName), Email, Data.FirstName, Data.LastName, Data.MiddleName, Data.BirthDay);
            var createModel = new CreateDoctorModel(info, Data.Specialization, Data.OfficeId, Data.CareerStartYear);
            try
            {
                var user = await DoctorService.CreateAsync(createModel, _cts.Token);
                await ImageService.CreateAsync(Data.Picture, _cts.Token);
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
        }
    }
}
