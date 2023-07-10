﻿using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Pages.Identity.Models;
using ClientsWebApp.Domain.Identity;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Identity
{
    public partial class Register : CancellableComponent
    {
        [Inject] public IAuthorizationService AuthorizationService { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }

        private RegisterData Data;
        protected FormSubmitButton SubmitButton { get; set; }
        private string ErrorMessage;
        protected override void OnInitialized()
        {
            Data = new RegisterData();
            ErrorMessage = "";
        }

        private async Task RegisterAsync()
        {
            SubmitButton.StartLoading();
            try
            {
                var user = await AuthorizationService.SingUpAsync(new SingUpModel(Data.Email, Data.Password, Data.RePassword), _cts.Token);
                await AuthorizationService.ConfirmEmailAsync(user.Id, _cts.Token);
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
            this.StateHasChanged();

            Navigation.NavigateTo("/login");
        }
    }
}
