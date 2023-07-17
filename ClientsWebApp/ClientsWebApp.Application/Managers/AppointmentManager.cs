using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Appointments;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Appointments;

namespace ClientsWebApp.Application.Managers
{
    public class AppointmentManager : IAppointmentManager
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentManager(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        public Task ApproveAsync(Guid id, CancellationToken cancellationToken)
        {
            return _appointmentService.ApproveAsync(id, cancellationToken);
        }

        public Task CancelAsync(Guid id, CancellationToken cancellationToken)
        {
            return _appointmentService.CancelAsync(id, cancellationToken);
        }

        public async Task ChangeDateAsync(ChangeDateData data, CancellationToken cancellationToken)
        {
            var rescheduleData = new RescheduleAppointmentModel(data.DoctorId, DateOnly.FromDateTime(data.Date), TimeOnly.FromDateTime(data.Date));
            await _appointmentService.RescheduleAsync(data.AppointmentId, rescheduleData, cancellationToken);
        }

        public async Task CreateAsync(CreateAppointmentData data, CancellationToken cancellationToken)
        {
            var model = new CreateAppointmentModel(data.PatientId, data.DoctorId, data.ServiceId, data.Date, data.Time);
            await _appointmentService.CreateAsync(model, cancellationToken);
        }

        public Task<Appointment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _appointmentService.GetByIdAsync(id, cancellationToken);
        }

        public Task<IEnumerable<Appointment>> GetPageAsync(Page page, AppointmentFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            return _appointmentService.GetPageAsync(page, filtrationModel, cancellationToken);
        }
    }
}
