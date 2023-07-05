using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Infrastructure;
using ClientsWebApp.Blazor.Pages.Specializations.Models;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Services;
using ClientsWebApp.Domain.Specializations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Specializations
{
    [Authorize(Roles = "Admin")]
    public partial class CreateSpecialization: CancellableComponent
    {
        [Inject] public ISpecializationService SpecializationService { get; set; }
        [Inject] public IServiceService ServiceService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private CreateSpecializationData Data { get; set; } = new CreateSpecializationData();

        private List<CreateServiceModel> Services = new List<CreateServiceModel>();
        private bool IsLoading = false;
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
            IsLoading = true;
            if(Services.Count  == 0) 
            {
                IsLoading = false;
                ErrorMessage = "Создайте по меньшей мере 1 сервис";
                return;
            }

            var createModel = new CreateSpecializationModel(Data.Name, Data.IsActive);
            try
            {
                var specialization = await SpecializationService.CreateAsync(createModel, _cts.Token);
                foreach(var service in Services)
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
            IsLoading = false;
            NavigationManager.NavigateTo("/specializations");
        }
    }
}
