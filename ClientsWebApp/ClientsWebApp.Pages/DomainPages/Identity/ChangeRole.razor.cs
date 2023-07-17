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

namespace ClientsWebApp.Pages.DomainPages.Identity
{
    public partial class ChangeRole
    {
        [Inject] IIdentityManager IdentityManager { get; set; }

        [Parameter] public string Email { get; set; }
        [Parameter] public EventCallback OnRoleChanged { get; set; }

        private bool IsRoleChanges { get; set; }
        private bool IsLoading { get; set; } = false;
        private List<string> Roles { get; set; } = new List<string>()
        {
            "Patient",
            "Admin",
            "Doctor"
        };
        private ChangeRoleData ChangeRoleData { get; set; }

        protected override void OnInitialized()
        {
            ChangeRoleData = new ChangeRoleData()
            {
                Role = Roles.First()
            };
        }

        private void StartChanging()
        {
            IsRoleChanges = true;
        }

        private async Task ChangeAsync()
        {
            IsLoading = true;

            await IdentityManager.ChangeRoleAsync(Email, ChangeRoleData, _cts.Token);
            await OnRoleChanged.InvokeAsync();

            IsRoleChanges = false;

            IsLoading = false;
        }
    }
}