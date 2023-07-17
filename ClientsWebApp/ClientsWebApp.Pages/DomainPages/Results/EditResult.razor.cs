﻿using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Results;
using ClientsWebApp.Domain.Results;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Results
{
    [Authorize(Roles = "Doctor")]
    public partial class EditResult : CancellableComponent
    {
        [Parameter] public Guid AppointmentId { get; set; }
        [Inject] public IResultManager ResultManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private FormSubmitButton SubmitButton { get; set; }

        private EditResultData Data { get; set; }
        private AppointmentResult Result { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Result = await ResultManager.GetForAppointmentAsync(AppointmentId, _cts.Token);
            Data = new EditResultData(Result);
        }
        private async Task EditAsync()
        {
            SubmitButton?.StartLoading();
            try
            {
                await ResultManager.EditAsync(Result.Id, Data, _cts.Token);
            }
            finally
            {
                SubmitButton?.StopLoading();
            }
            Cancel();
        }
        private void Cancel()
        {
            NavigationManager.NavigateTo($"/appointments/{AppointmentId}");
        }
    }
}