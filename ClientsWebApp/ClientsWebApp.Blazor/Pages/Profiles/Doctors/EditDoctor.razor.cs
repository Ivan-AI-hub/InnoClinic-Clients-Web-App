using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Specializations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Doctors
{
    [Authorize(Roles = "Doctor")]
    public partial class EditDoctor : CancellableComponent
    {
        [Parameter] public DoctorDTO? OldDoctor { get; set; }
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IDoctorManager DoctorManager { get; set; }
        [Inject] public ISpecializationManager SpecializationManager { get; set; }
        [Inject] public IOfficeManager OfficeManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private EditDoctorData Data { get; set; }
        private IEnumerable<Specialization> Specializations { get; set; }
        private IEnumerable<Office> Offices { get; set; }

        private string ErrorMessage;
        private string Email;
        protected override async Task OnInitializedAsync()
        {
            var page = new Page(100, 1);
            if (OldDoctor == null)
            {
                Email = await authStateHelper.GetEmailAsync();
                OldDoctor = await DoctorManager.GetByEmailAsync(Email, _cts.Token);
            }
            Data = new EditDoctorData(OldDoctor);
            Specializations = await SpecializationManager.GetInfoAsync(page, _cts.Token);
            Offices = await OfficeManager.GetInfoPageAsync(page, _cts.Token);

            if (OldDoctor.Specialization == null)
            {
                Data.Specialization = Specializations.Select(x => x.Name).First();
            }
        }

        private async Task EditAsync()
        {
            SubmitButton?.StartLoading();

            try
            {
                await DoctorManager.EditAsync(OldDoctor, Data, _cts.Token);
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
