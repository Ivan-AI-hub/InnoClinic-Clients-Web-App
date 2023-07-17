using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Results;
using ClientsWebApp.Domain.Results;

namespace ClientsWebApp.Application.Managers
{
    public class ResultManager : IResultManager
    {
        private readonly IResultService _resultService;

        public ResultManager(IResultService resultService)
        {
            _resultService = resultService;
        }

        public async Task CreateAsync(CreateResultData data, CancellationToken cancellationToken)
        {
            var createModel = new CreateResultModel(data.Complaints, data.Conclusion, data.Recomendations, data.AppointmentId, data.PatientId, data.DoctorId);

            await _resultService.CreateAsync(createModel, cancellationToken);
        }

        public async Task EditAsync(Guid id, EditResultData data, CancellationToken cancellationToken)
        {
            var editModel = new UpdateResultModel(data.Complaints, data.Conclusion, data.Recomendations);

            await _resultService.UpdateAsync(id, editModel, cancellationToken);
        }

        public Task<AppointmentResult> GetForAppointmentAsync(Guid appointmentId, CancellationToken cancellationToken)
        {
            return _resultService.GetForAppointmentAsync(appointmentId, cancellationToken);
        }
    }
}
