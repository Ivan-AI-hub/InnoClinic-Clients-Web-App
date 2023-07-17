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

namespace ClientsWebApp.Pages.DomainPages.Appointments
{
    public partial class ChangeAppointmentDate
    {
        [Parameter]
        public Appointment Appointment { get; set; }

        [Parameter]
        public EventCallback OnDateChanged { get; set; }

        private bool IsDateChanges { get; set; }

        private bool IsLoading { get; set; } = false;
        private ChangeDateData Data { get; set; }

        protected override void OnInitialized()
        {
            Data = new ChangeDateData()
            {
                Date = Appointment.Date.ToDateTime(Appointment.Time),
                AppointmentId = Appointment.Id,
                DoctorId = Appointment.Doctor.Id
            };
        }

        private void StartChanging()
        {
            IsDateChanges = true;
        }

        private void StopChanging()
        {
            IsDateChanges = false;
        }

        private async Task ChangeAsync()
        {
            IsLoading = true;
            await AppointmentManager.ChangeDateAsync(Data, _cts.Token);
            await OnDateChanged.InvokeAsync();
            StopChanging();
            IsLoading = false;
        }
    }
}