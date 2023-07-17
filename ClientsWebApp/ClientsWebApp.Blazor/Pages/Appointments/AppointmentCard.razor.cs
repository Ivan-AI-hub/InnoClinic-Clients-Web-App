using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Internal;
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
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Application.Models.Appointments;
using ClientsWebApp.Blazor.Pages.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using ClientsWebApp.Application.Abstraction;
using Microsoft.AspNetCore.Http;
using ClientsWebApp.Application.Models.Receptionists;
using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Application.Models.Offices;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Application.Models.Services;
using System.Diagnostics.CodeAnalysis;
using ClientsWebApp.Blazor.Pages.Results;
using ClientsWebApp.Blazor.Pages.Appointments;
using ClientsWebApp.Blazor.Shared;

namespace ClientsWebApp.Blazor.Pages.Appointments
{
    public partial class AppointmentCard
    {
        [Parameter]
        public Appointment Appointment { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

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