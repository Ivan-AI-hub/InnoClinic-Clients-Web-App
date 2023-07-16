using ClientsWebApp.Application.Models.Results;
using ClientsWebApp.Domain.Results;

namespace ClientsWebApp.Application.Abstraction
{
    internal interface IResultManager
    {
        public Task<ManagerResult> CreateAsync(CreateResultData data, CancellationToken cancellationToken);
        public Task<ManagerResult> EditAsync(EditResultData data, CancellationToken cancellationToken);
        public Task<AppointmentResult> GetForAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken);
    }
}
