using Blazored.Modal;
using Blazored.Modal.Services;
using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Services;
using ClientsWebApp.Application.Models.Specializations;
using ClientsWebApp.Blazor.Components;
using ClientsWebApp.Blazor.Pages.Services;
using ClientsWebApp.Domain.Services;
using ClientsWebApp.Domain.Specializations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace ClientsWebApp.Blazor.Pages.Specializations
{
    [Authorize(Roles = "Admin")]
    public partial class EditSpecialization : CancellableComponent
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Parameter] public Specialization? OldSpecialization { get; set; }
        [Parameter] public Guid SpecializationId { get; set; }
        [Inject] public ISpecializationManager SpecializationManager { get; set; }
        [Inject] public IServiceManager ServiceManager { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        private EditSpecializationData Data { get; set; }

        private List<Service> Services = new List<Service>();
        private List<CreateServiceData> AddedServices = new List<CreateServiceData>();
        private List<Service> RemovedServices = new List<Service>();

        private FormSubmitButton SubmitButton { get; set; }
        private bool IsManagerCreating = false;
        private string ErrorMessage;
        private IModalReference? _openedModal;
        protected async override Task OnInitializedAsync()
        {
            OldSpecialization = OldSpecialization ?? await SpecializationManager.GetByIdAsync(SpecializationId, _cts.Token);
            Data = new EditSpecializationData(OldSpecialization);
            Services = OldSpecialization.Services;
        }

        private void StartCreateService()
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(CreateService.SpecializationId), default(Guid));
            parameters.Add(nameof(CreateService.OnModelCreated), EventCallback.Factory.Create<CreateServiceData>(this, StopCreateService));
            _openedModal = Modal.Show<CreateService>("Create service", parameters);
        }
        private void StopCreateService(CreateServiceData model)
        {
            AddedServices.Add(model);
            _openedModal?.Close();
        }

        private void RemoveService(CreateServiceData model)
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
            SubmitButton?.StartLoading();
            if (Services.Count + AddedServices.Count == 0)
            {
                SubmitButton?.StopLoading();
                ErrorMessage = "Create at least 1 service";
                return;
            }

            try
            {
                await SpecializationManager.EditAsync(SpecializationId, Data, _cts.Token);
                //foreach (var service in RemovedServices)
                //{
                //    await ServiceManager.Remove(service, _cts.Token);
                //}
                foreach (var service in AddedServices)
                {
                    service.SpecializationId = OldSpecialization.Id;
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
            NavigationManager.NavigateTo($"/specializations/{SpecializationId}");
        }
    }
}
