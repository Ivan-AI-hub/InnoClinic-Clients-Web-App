namespace ClientsWebApp.Domain.Appointments
{
    public interface IAppointmentService
    {
        public Task<Appointment> CreateAsync(CreateAppointmentModel model, CancellationToken cancellationTokend);
        public Task ApproveAsync(Guid id, CancellationToken cancellationTokend);
        public Task<IEnumerable<Appointment>> GetPageAsync(Page page, AppointmentFiltrationModel filtrationModel, CancellationToken cancellationTokend);
        public Task DeleteAsync(Guid id, CancellationToken cancellationTokend);
        public Task RescheduleAsync(Guid id, RescheduleAppointmentModel model, CancellationToken cancellationTokend);
    }
}
