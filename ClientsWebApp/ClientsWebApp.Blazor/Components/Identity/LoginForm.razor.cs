using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Identity;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Components.Identity;

public partial class LoginForm : CancellableComponent
{
    [Inject] public IIdentityManager identityManager { get; set; }
    [Inject] public NavigationManager Navigation { get; set; }

    protected LoginData LoginData { get; set; }
    protected FormSubmitButton SubmitButton { get; set; }
    protected string ErrorMessage { get; set; }

    protected override void OnInitialized()
    {
        LoginData = new LoginData();
        ErrorMessage = "";
    }

    protected async Task LoginAsync()
    {
        SubmitButton.StartLoading();
        try
        {
            await identityManager.SingInAsync(LoginData, _cts.Token);
            Navigation.NavigateTo("/", true);
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