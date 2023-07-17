using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Doctors;
using Blazored.Modal.Services;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Blazored.Modal;

namespace ClientsWebApp.Blazor.Pages.Profiles.Doctors
{
    [Authorize(Roles = "Doctor")]
    public partial class DoctorHome : CancellableComponent
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Inject] AuthenticationStateHelper StateHelper { get; set; }
        [Inject] IDoctorManager DoctorManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private DoctorDTO? Doctor { get; set; }
        private bool IsLoading { get; set; } = true;
        protected override async void OnInitialized()
        {
            IsLoading = true;
            var email = await StateHelper.GetEmailAsync();
            try
            {
                Doctor = await DoctorManager.GetByEmailAsync(email, _cts.Token);
            }
            catch (NotFoundException ex)
            {
                NavigateToCreatePage();
            }
            IsLoading = false;
            StateHasChanged();
        }

        private void NavigateToEditPage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large,
                DisableBackgroundCancel = true
            };
            Modal.Show<EditDoctor>("Edit profile", options);
            //NavigationManager.NavigateTo("/doctors/edit");
        }
        private async Task NavigateToCreatePage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large,
                DisableBackgroundCancel = true
            };
            Modal.Show<CreateDoctor>("Create Profile", options);
            //NavigationManager.NavigateTo("/doctors/create");
        }
        private void NavigateToSchedulePage()
        {
            NavigationManager.NavigateTo($"/doctors/{Doctor.Id}/schedule");
        }

        private void Reload()
        {
            NavigationManager.NavigateTo($"/");
        }
    }
}
