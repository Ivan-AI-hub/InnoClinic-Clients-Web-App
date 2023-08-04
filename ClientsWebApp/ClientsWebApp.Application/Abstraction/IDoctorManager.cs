using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Profiles.Doctor;

namespace ClientsWebApp.Application.Abstraction
{
    public interface IDoctorManager
    {
        public Task ChangeStatusAsync(Guid id, ChangeDoctorStatusData data, CancellationToken cancellationToken);
        public Task CreateAsync(CreateDoctorData data, CancellationToken cancellationToken);
        public Task EditAsync(DoctorDTO oldDoctor, EditDoctorData data, CancellationToken cancellationToken);
        public Task<IEnumerable<DoctorDTO>> GetPageAsync(Page page, DoctorFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<IEnumerable<Doctor>> GetInfoPageAsync(Page page, DoctorFiltrationModel filtrationModel, CancellationToken cancellationToken);
        public Task<DoctorDTO> GetByEmailAsync(string email, CancellationToken cancellationToken);
        public Task<DoctorDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
