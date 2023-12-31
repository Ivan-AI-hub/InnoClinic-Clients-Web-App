﻿using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Specializations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Specializations
{
    [Authorize(Roles = "Admin")]
    public partial class SpecializationsList : CancellableComponent
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Inject] public ISpecializationManager SpecializationManager { get; set; }

        private Page Page { get; set; }
        private IEnumerable<Specialization>? Specializations { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Page = new Page(15, 1);
            await SpecializationsUpdateAsync();
            StateHasChanged();
        }

        private async Task SpecializationsUpdateAsync()
        {
            Specializations = null;
            StateHasChanged();
            Specializations = await SpecializationManager.GetInfoAsync(Page, _cts.Token);
            StateHasChanged();
        }

        protected async Task SetPreviousPage()
        {
            Page.Number--;
            await SpecializationsUpdateAsync();
        }
        protected async Task SetNextPage()
        {
            Page.Number++;
            await SpecializationsUpdateAsync();
        }
        protected PageStatus GetPageStatus()
        {
            return Page.GetPageStatus(Specializations == null ? 0 : Specializations.Count());
        }
        private async Task ToCreatePage()
        {
            var options = new ModalOptions()
            {
                Size = ModalSize.Large
            };
            await Modal.Show<CreateSpecialization>("Create", options).Result;
            await SpecializationsUpdateAsync();
        }
    }
}
