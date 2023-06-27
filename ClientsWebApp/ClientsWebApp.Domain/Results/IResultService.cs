namespace ClientsWebApp.Domain.Results
{
    public interface IResultService
    {
        public Task<AppointmentResult> CreateAsync(CreateResultModel model, CancellationToken cancellationTokend);
        public Task UpdateAsync(Guid id, UpdateResultModel model, CancellationToken cancellationTokend);
        public Task<AppointmentResult> GetForAppointmentAsync(Guid appointmentId, CancellationToken cancellationTokend);
    }
}
