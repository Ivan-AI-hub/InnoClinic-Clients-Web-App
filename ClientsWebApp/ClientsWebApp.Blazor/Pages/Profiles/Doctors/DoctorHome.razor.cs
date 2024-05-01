using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Exceptions;
using ClientsWebApp.Domain.Profiles.Patient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

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
        [Inject] private IAppointmentManager _appointmentManager { get; set; }
        private IEnumerable<Appointment>? Appointments { get; set; }
        private Page Page { get; set; } = new Page(20, 1);
        protected override async void OnInitialized()
        {
            IsLoading = true;
            var email = await StateHelper.GetEmailAsync();
            try
            {
                Doctor = await DoctorManager.GetByEmailAsync(email, _cts.Token);
            }
            catch (NotFoundException)
            {
                NavigateToCreatePage();
            }

            var filtrationModel = new AppointmentFiltrationModel
            {
                DoctorId = Doctor.Id,
                Status = null
            };
            Appointments = await _appointmentManager.GetPageAsync(Page, filtrationModel, _cts.Token);

            IsLoading = false;
            StateHasChanged();
        }

        private void NavigateToEditPage()
        {
            var options = new ModalOptions() 
            {
                Size = ModalSize.Large
            };
            var parameters = new ModalParameters();
            parameters.Add(nameof(EditDoctor.OldDoctor), Doctor);
            Modal.Show<EditDoctor>("Edit profile", parameters, options);
            //NavigationManager.NavigateTo("/doctors/edit");
        }
        private void NavigateToCreatePage()
        {
            var options = new ModalOptions() 
            {
                Size = ModalSize.Large
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
