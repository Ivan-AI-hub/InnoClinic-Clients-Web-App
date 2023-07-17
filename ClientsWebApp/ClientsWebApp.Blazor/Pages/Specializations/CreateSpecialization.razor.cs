using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Services;
using ClientsWebApp.Application.Models.Specializations;
using ClientsWebApp.Blazor.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Specializations
{
    [Authorize(Roles = "Admin")]
    public partial class CreateSpecialization : CancellableComponent
    {
        [Inject] public ISpecializationManager SpecializationManager { get; set; }
        [Inject] public IServiceManager ServiceManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private CreateSpecializationData Data { get; set; } = new CreateSpecializationData();

        private List<CreateServiceData> Services = new List<CreateServiceData>();
        private FormSubmitButton SubmitButton { get; set; }
        private bool IsManagerCreating = false;
        private string ErrorMessage;

        private void StartCreateService()
        {
            IsManagerCreating = true;
        }
        private void StopCreateService(CreateServiceData model)
        {
            Services.Add(model);
            IsManagerCreating = false;
        }

        private void RemoveService(CreateServiceData model)
        {
            Services.Remove(model);
        }

        private async Task CreateAsync()
        {
            SubmitButton?.StartLoading();

            if (Services.Count == 0)
            {
                SubmitButton?.StopLoading();
                ErrorMessage = "Создайте по меньшей мере 1 сервис";
                return;
            }

            try
            {
                var specialization = await SpecializationManager.CreateAsync(Data, _cts.Token);
                foreach (var service in Services)
                {
                    service.SpecializationId = specialization.Id;
                    await ServiceManager.CreateAsync(service, _cts.Token);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            finally
            {
                SubmitButton?.StopLoading();
            }

            Cancel();
        }
        private void Cancel()
        {
            NavigationManager.NavigateTo("/specializations");
        }
    }
}
