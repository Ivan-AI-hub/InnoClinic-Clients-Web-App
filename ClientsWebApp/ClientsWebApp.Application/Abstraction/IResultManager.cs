using ClientsWebApp.Application.Models.Results;
using ClientsWebApp.Domain.Results;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IResultManager
    {
        public Task CreateAsync(CreateResultData data, CancellationToken cancellationToken);
        public Task EditAsync(Guid id, EditResultData data, CancellationToken cancellationToken);
        public Task<AppointmentResult> GetForAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken);
    }
}
