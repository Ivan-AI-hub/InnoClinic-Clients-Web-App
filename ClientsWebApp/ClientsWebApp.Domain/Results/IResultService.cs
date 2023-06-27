namespace ClientsWebApp.Domain.Results
{
    public interface IResultService
    {
        public Task<AppointmentResult> CreateAsync(CreateResultModel model, CancellationToken cancellationToken);
        public Task UpdateAsync(Guid id, UpdateResultModel model, CancellationToken cancellationToken);
        public Task<AppointmentResult> GetForAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken);
    }
}
