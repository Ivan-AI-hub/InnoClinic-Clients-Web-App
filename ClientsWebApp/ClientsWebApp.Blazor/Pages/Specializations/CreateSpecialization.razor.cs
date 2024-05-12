using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Services;
using ClientsWebApp.Application.Models.Specializations;
using ClientsWebApp.Blazor.AppLocalization;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Pages.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Specializations
{
    [Authorize(Roles = "Admin")]
    public partial class CreateSpecialization : CancellableComponent
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Inject] public ISpecializationManager SpecializationManager { get; set; }
        [Inject] public IServiceManager ServiceManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private CreateSpecializationData Data { get; set; } = new CreateSpecializationData();

        private List<CreateServiceData> Services = new List<CreateServiceData>();
        private FormSubmitButton SubmitButton { get; set; }
        private string ErrorMessage;
        private IModalReference? _openedModal;

        private void StartCreateService()
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(CreateService.SpecializationId), default(Guid));
            parameters.Add(nameof(CreateService.OnModelCreated), EventCallback.Factory.Create<CreateServiceData>(this, StopCreateService));
            _openedModal = Modal.Show<CreateService>(_localizer.GetString(LocalizationType.CreateServiceButtonValue), parameters);
        }
        private void StopCreateService(CreateServiceData model)
        {
            Services.Add(model);
            _openedModal?.Close();
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
                ErrorMessage = @_localizer.GetString(LocalizationType.CreateSpecializationError);
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
