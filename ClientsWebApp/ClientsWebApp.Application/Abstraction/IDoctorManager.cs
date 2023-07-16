using ClientsWebApp.Application.Models.Appointments;
using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Domain.Profiles.Doctor;
using ClientsWebApp.Domain;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IDoctorManager
    {
        public Task<ManagerResult> ChangeStatusAsync(ChangeDoctorStatusData data, CancellationToken cancellationToken);
        public Task<ManagerResult> CreateAsync(CreateDoctorData data, CancellationToken cancellationToken);
        public Task<ManagerResult> EditAsync(EditDoctorData data, CancellationToken cancellationToken);
        public Task<IEnumerable<Doctor>> GetPageAsync(Page page, DoctorFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<Doctor> GetByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<Doctor> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
