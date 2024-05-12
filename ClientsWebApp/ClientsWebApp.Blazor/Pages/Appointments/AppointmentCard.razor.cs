using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Results;
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
        private bool _hasResult;

        protected async override Task OnInitializedAsync()
        {
            service = await ServiceManager.GetByIdAsync(Appointment.Service.Id, _cts.Token);
        }
        private Task Update()
        {
            return OnDataUpdated.InvokeAsync();
        }
        private void ResultAvailable(AppointmentResult appointmentResult)
        {
            _hasResult = true;
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