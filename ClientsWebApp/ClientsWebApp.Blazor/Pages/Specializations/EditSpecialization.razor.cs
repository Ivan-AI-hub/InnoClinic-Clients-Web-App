using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Infrastructure;
using ClientsWebApp.Blazor.Pages.Services.Models;
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
    public partial class EditSpecialization: CancellableComponent
    {
        [Parameter] public Guid SpecializationId { get; set; }
        [Inject] public ISpecializationService SpecializationService { get; set; }
        [Inject] public IServiceService ServiceService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private EditSpecializationData Data { get; set; }

        private Specialization OldSpecialization { get; set; }
        private List<Service> Services = new List<Service>();
        private List<CreateServiceModel> AddedServices = new List<CreateServiceModel>();
        private List<Service> RemovedServices = new List<Service>();

        private bool IsLoading = false;
        private bool IsServiceCreating = false;
        private string ErrorMessage;
        protected async override Task OnInitializedAsync()
        {
            OldSpecialization = await SpecializationService.GetByIdAsync(SpecializationId, _cts.Token);
            Data = new EditSpecializationData(OldSpecialization);
            Services = OldSpecialization.Services;
        }

        private void StartCreateService()
        {
            IsServiceCreating = true;
        }
        private void StopCreateService(CreateServiceModel model)
        {
            AddedServices.Add(model);
            IsServiceCreating = false;
        }

        private void RemoveService(CreateServiceModel model)
        {
            AddedServices.Remove(model);
        }
        private void RemoveService(Service model)
        {
            Services.Remove(model);
            RemovedServices.Add(model);
        }

        private async Task EditAsync()
        {
            IsLoading = true;
            if (Services.Count + AddedServices.Count == 0)
            {
                IsLoading = false;
                ErrorMessage = "Создайте по меньшей мере 1 сервис";
                return;
            }

            var updateModel = new UpdateSpecializationModel(Data.Name, Data.IsActive);
            try
            {
                await SpecializationService.UpdateAsync(SpecializationId, updateModel, _cts.Token);
                //foreach (var service in RemovedServices)
                //{
                //    await ServiceService.Remove(service, _cts.Token);
                //}
                foreach (var service in AddedServices)
                {
                    service.SpecializationId = OldSpecialization.Id;
                    await ServiceService.CreateAsync(service, _cts.Token);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return;
            }
            IsLoading = false;
            NavigationManager.NavigateTo($"/specializations/{SpecializationId}");
        }
    }
}
