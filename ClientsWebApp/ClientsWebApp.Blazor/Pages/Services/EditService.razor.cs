﻿using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Services;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Services
{
    [Authorize(Roles = "Admin")]
    public partial class EditService : CancellableComponent
    {
        [Parameter] public Service? OldService { get; set; }
        [Parameter] public Guid SpecializationId { get; set; }
        [Parameter] public Guid ServiceId { get; set; }
        [Inject] public IServiceManager ServiceManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }
        private EditServiceData? Data { get; set; }
        private List<string> Categories = new List<string>() { "consultation", "analyses", "diagnostics" };

        protected override async Task OnInitializedAsync()
        {
            var oldService = OldService ?? await ServiceManager.GetByIdAsync(ServiceId, _cts.Token);
            Data = new EditServiceData(oldService);
        }
        private async Task EditAsync()
        {
            SubmitButton?.StartLoading();

            try
            {
                await ServiceManager.EditAsync(ServiceId, Data, _cts.Token);
            }
            finally
            {
                SubmitButton?.StopLoading();
            }

            Cancel();
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo($"/specializations/{SpecializationId}");
        }

    }
}
