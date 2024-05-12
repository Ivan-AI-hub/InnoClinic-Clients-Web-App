using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Blazor.Components.Identity;
using ClientsWebApp.Blazor.Pages.Appointments;
using ClientsWebApp.Domain.Services;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Doctors
{
    public partial class DoctorInfo
    {
        [Inject] AuthenticationStateHelper StateHelper { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; }

        [Parameter] public Guid DoctorId { get; set; }

        private string? _role;
        private string _searchString;

        private DoctorDTO doctor;
        private IEnumerable<Service>? Services { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var authorized = await StateHelper.IsAuthenticatedAsync();
            Console.WriteLine($"Authorized: {authorized}");
            if(authorized)
            {
                _role = await StateHelper.GetRoleAsync();
                Console.WriteLine($"Role: {_role}");

            }

            doctor = await DoctorManager.GetByIdAsync(DoctorId, _cts.Token);
            Services = await ServiceManager.GetBySpecializationAsync(doctor.Specialization, _cts.Token);

            StateHasChanged();
        }

        private void ToCreateAppointmentPage(Guid serviceId, string category)
        {
            if(_role is null)
            {
                var LoginOptions = new ModalOptions()
                {
                    HideCloseButton = false,
                    DisableBackgroundCancel = false
                };
                Modal.Show<LoginForm>("Sign in", LoginOptions);

                return;
            }

            var CreateAppointmentOptions = new ModalOptions()
            {
                Size = ModalSize.Large,
                HideCloseButton = false
            };
            var parameters = new ModalParameters
            {
                { nameof(CreateAppointment.InitialDoctorId), doctor.Id },
                { nameof(CreateAppointment.InitialServiceId), serviceId },
                { nameof(CreateAppointment.InitialSpecialization), doctor.Specialization },
                { nameof(CreateAppointment.InitialCategory), category },
            };
            Modal.Show<CreateAppointment>("Create appointment", parameters, CreateAppointmentOptions);
        }

        private Func<Service, bool> _quickFilter => x =>
        {
            if (string.IsNullOrWhiteSpace(_searchString))
            {
                return true;
            }

            if (x.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (x.Category is null)
            {
                return false;
            }

            if(x.Category.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        };
    }
}