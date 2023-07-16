using ClientsWebApp.Domain.Services;
using ClientsWebApp.Domain.Specializations;
using ClientsWebApp.Pages.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Pages.DomainPages.Specializations
{
    [Authorize(Roles = "Admin")]
    public partial class CreateSpecialization : CancellableComponent
    {
        [Inject] public ISpecializationService SpecializationService { get; set; }
        [Inject] public IServiceService ServiceService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private CreateSpecializationData Data { get; set; } = new CreateSpecializationData();

        private List<CreateServiceModel> Services = new List<CreateServiceModel>();
        private FormSubmitButton SubmitButton { get; set; }
        private bool IsServiceCreating = false;
        private string ErrorMessage;

        private void StartCreateService()
        {
            IsServiceCreating = true;
        }
        private void StopCreateService(CreateServiceModel model)
        {
            Services.Add(model);
            IsServiceCreating = false;
        }

        private void RemoveService(CreateServiceModel model)
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

            var createModel = new CreateSpecializationModel(Data.Name, Data.IsActive);
            try
            {
                var specialization = await SpecializationService.CreateAsync(createModel, _cts.Token);
                foreach (var service in Services)
                {
                    service.SpecializationId = specialization.Id;
                    await ServiceService.CreateAsync(service, _cts.Token);
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
