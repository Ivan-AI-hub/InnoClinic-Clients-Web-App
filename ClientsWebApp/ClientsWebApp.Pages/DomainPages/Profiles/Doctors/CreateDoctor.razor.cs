﻿using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain.Specializations;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Profiles.Doctors
{
    [Authorize(Roles = "Doctor")]
    public partial class CreateDoctor : CancellableComponent
    {
        [Inject] public AuthenticationStateHelper authStateHelper { get; set; }
        [Inject] public IDoctorManager DoctorManager { get; set; }
        [Inject] public ISpecializationManager SpecializationManager { get; set; }
        [Inject] public IOfficeManager OfficeManager { get; set; }
        private CreateDoctorData Data { get; set; }
        protected FormSubmitButton SubmitButton { get; set; }
        private IEnumerable<Specialization> Specializations { get; set; }
        private IEnumerable<Office> Offices { get; set; }
        private string ErrorMessage;
        protected override async Task OnInitializedAsync()
        {
            var page = new Page(100, 1);
            Data = new CreateDoctorData();
            Data.Email = await authStateHelper.GetEmailAsync();
            Specializations = await SpecializationManager.GetInfoAsync(page, _cts.Token);
            Offices = await OfficeManager.GetPageAsync(page, _cts.Token);
        }

        private async Task CreateAsync()
        {
            SubmitButton.StartLoading();
            
            try
            {
                await DoctorManager.CreateAsync(Data, _cts.Token);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            finally
            {
                SubmitButton.StopLoading();
            }
        }
    }
}
