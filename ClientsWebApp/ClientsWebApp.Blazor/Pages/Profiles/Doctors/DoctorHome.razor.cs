using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Blazor;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Doctors
{
    [Authorize(Roles = "Doctor")]
    public partial class DoctorHome : CancellableComponent
    {
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
                NavigationManager.NavigateTo("/doctors/create");
            }
            IsLoading = false;
            StateHasChanged();
        }

        private void NavigateToEditPage()
        {
            NavigationManager.NavigateTo("/doctors/edit");
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
