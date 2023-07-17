using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Blazored.FluentValidation;
using Blazored;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Authorization;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain.Categories;
using ClientsWebApp.Domain.Documents;
using ClientsWebApp.Domain.Identity;
using ClientsWebApp.Domain.Identity.HttpClients;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Domain.Profiles.Receptionist;
using ClientsWebApp.Domain.Results;
using ClientsWebApp.Domain.Services;
using ClientsWebApp.Domain.Specializations;
using ClientsWebApp.Application.Models.Identity;
using ClientsWebApp.Domain;
using System.Security.Claims;
using ClientsWebApp.Pages.Components;
using ClientsWebApp.Application.Models.Appointments;
using ClientsWebApp.Pages.DomainPages.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using ClientsWebApp.Application.Abstraction;
using Microsoft.AspNetCore.Http;
using ClientsWebApp.Application.Models.Doctors;

namespace ClientsWebApp.Pages.DomainPages.Profiles.Doctors
{
    public partial class DoctorInfo
    {
        [Parameter]
        public Guid DoctorId { get; set; }

        private DoctorDTO doctor;
        private IEnumerable<Service>? Services { get; set; }

        protected async override Task OnInitializedAsync()
        {
            doctor = await DoctorManager.GetByIdAsync(DoctorId, _cts.Token);
            Services = await ServiceManager.GetBySpecializationAsync(doctor.Specialization, _cts.Token);
            this.StateHasChanged();
        }

        private void ToCreateAppointmentPage()
        {
            NavigationManager.NavigateTo($"/appointments/create?DoctorId={doctor.Id}");
        }
    }
}