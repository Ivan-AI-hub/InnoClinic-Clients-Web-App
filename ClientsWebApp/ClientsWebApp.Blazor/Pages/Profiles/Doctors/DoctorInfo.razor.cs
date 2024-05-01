using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Domain.Services;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Profiles.Doctors
{
    public partial class DoctorInfo
    {
        [Parameter] public Guid DoctorId { get; set; }

        private string _searchString;

        private DoctorDTO doctor;
        private IEnumerable<Service>? Services { get; set; }

        protected async override Task OnInitializedAsync()
        {
            doctor = await DoctorManager.GetByIdAsync(DoctorId, _cts.Token);
            Services = await ServiceManager.GetBySpecializationAsync(doctor.Specialization, _cts.Token);
            StateHasChanged();
        }

        private void ToCreateAppointmentPage()
        {
            NavigationManager.NavigateTo($"/appointments/create?DoctorId={doctor.Id}");
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