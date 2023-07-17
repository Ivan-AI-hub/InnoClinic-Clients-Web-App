using ClientsWebApp.Application.Models.Appointments;
using ClientsWebApp.Domain.Appointments;
using ClientsWebApp.Domain;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IAppointmentManager
    {
        public Task ChangeDateAsync(ChangeDateData data, CancellationToken cancellationToken);
        public Task CreateAsync(CreateAppointmentData data, CancellationToken cancellationToken);
        public Task ApproveAsync(Guid id, CancellationToken cancellationToken);
        public Task<IEnumerable<Appointment>> GetPageAsync(Page page, AppointmentFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<Appointment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task CancelAsync(Guid id, CancellationToken cancellationToken);
    }
}
