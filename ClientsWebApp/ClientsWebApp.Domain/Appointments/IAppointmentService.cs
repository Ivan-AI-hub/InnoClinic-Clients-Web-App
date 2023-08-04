namespace ClientsWebApp.Domain.Appointments
{
    public interface IAppointmentService
    {
        public Task<Appointment> CreateAsync(CreateAppointmentModel model, CancellationToken cancellationToken);
        public Task ApproveAsync(Guid id, CancellationToken cancellationToken);
        public Task<IEnumerable<Appointment>> GetPageAsync(Page page, AppointmentFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<Appointment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        public Task CancelAsync(Guid id, CancellationToken cancellationToken);
        public Task RescheduleAsync(Guid id, RescheduleAppointmentModel model, CancellationToken cancellationToken);
    }
}
