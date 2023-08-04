using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Services;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    public partial class AppointmentCard
    {
        [Parameter] public EventCallback OnDataUpdated { get; set; }
        [Parameter] public Appointment Appointment { get; set; }
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Inject] public IServiceManager ServiceManager { get; set; }

        private Service? service;
        protected async override Task OnInitializedAsync()
        {
            service = await ServiceManager.GetByIdAsync(Appointment.Service.Id, _cts.Token);
        }
        private Task Update()
        {
            return OnDataUpdated.InvokeAsync();
        }
        private void NavigateToDoctorPage()
        {

            NavigationManager.NavigateTo($"/doctors/{Appointment.Doctor.Id}");
        }

        private void NavigateToPatientPage()
        {
            NavigationManager.NavigateTo($"/patients/{Appointment.Patient.Id}");
        }
    }
}