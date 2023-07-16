using ClientsWebApp.Application.Models.Appointments;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IAppointmentManager
    {
        public Task<ManagerResult> ChangeDateAsync(ChangeDateData data, CancellationToken cancellationToken);
        public Task<ManagerResult> CreateAsync(CreateAppointmentData data, CancellationToken cancellationToken);
        public Task<ManagerResult> ApproveAsync(Guid id, CancellationToken cancellationToken);
        public Task<IEnumerable<Appointment>> GetPageAsync(Page page, AppointmentFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<Appointment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task<ManagerResult> CancelAsync(Guid id, CancellationToken cancellationToken);
    }
}
