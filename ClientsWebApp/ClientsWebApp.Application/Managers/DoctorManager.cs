using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Doctors;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles;
using ClientsWebApp.Domain.Profiles.Doctor;

namespace ClientsWebApp.Application.Managers
{
    public class DoctorManager : IDoctorManager
    {
        private readonly IDoctorService _doctorService;
        private readonly IImageService _imageService;
        private readonly IOfficeService _officeService;

        public DoctorManager(IDoctorService doctorService, IImageService imageService, IOfficeService officeService)
        {
            _doctorService = doctorService;
            _imageService = imageService;
            _officeService = officeService;
        }

        public async Task ChangeStatusAsync(Guid id, ChangeDoctorStatusData data, CancellationToken cancellationToken)
        {
            var changestatusModel = new ChangeDoctorStatusModel(data.Status);
            await _doctorService.UpdateStatusAsync(id, changestatusModel, cancellationToken);
        }

        public async Task CreateAsync(CreateDoctorData data, CancellationToken cancellationToken)
        {
            if(data.BirthDay is null)
            {
                return;
            }

            var info = new CreateHumanInfo(new ImageName(data.Picture.FileName), data.Email, data.FirstName, data.LastName, data.MiddleName, data.BirthDay.Value);
            var createModel = new CreateDoctorModel(info, data.Specialization, data.OfficeId, data.CareerStartYear);

            await _doctorService.CreateAsync(createModel, cancellationToken);
            await _imageService.CreateAsync(data.Picture, cancellationToken);
        }

        public async Task EditAsync(DoctorDTO oldDoctor, EditDoctorData data, CancellationToken cancellationToken)
        {
            if(data.BirthDay is null)
            {
                return;
            }

            var fileName = (await oldDoctor.Info.ToHumanInfo()).Photo.Name; 
            var imageName = data.Picture == null ? new ImageName(fileName) : new ImageName(data.Picture.FileName);
            var updateModel = new UpdateDoctorModel(imageName, data.FirstName, data.LastName, data.MiddleName, data.BirthDay.Value, data.Specialization, data.OfficeId, data.CareerStartYear, data.Status);

            await _doctorService.UpdateAsync(oldDoctor.Id, updateModel, cancellationToken);
            if (data.Picture != null && data.Picture.FileName != fileName)
            {
                await _imageService.DeleteAsync(fileName, cancellationToken);
                await _imageService.CreateAsync(data.Picture, cancellationToken);
            }

        }

        public async Task<DoctorDTO> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var doctorData = await _doctorService.GetByEmailAsync(email, cancellationToken);
            return await DoctorToDoctorDTOConvertor(doctorData, cancellationToken);
        }

        public async Task<DoctorDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var doctorData = await _doctorService.GetByIdAsync(id, cancellationToken);
            return await DoctorToDoctorDTOConvertor(doctorData, cancellationToken);
        }

        public async Task<IEnumerable<DoctorDTO>> GetPageAsync(Page page, DoctorFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            var doctorsData = await _doctorService.GetPageAsync(page, filtrationModel, cancellationToken);
            var doctors = new List<DoctorDTO>();
            foreach (var doctor in doctorsData)
            {
                doctors.Add(await DoctorToDoctorDTOConvertor(doctor, cancellationToken));
            }
            return doctors;
        }

        public Task<IEnumerable<Doctor>> GetInfoPageAsync(Page page, DoctorFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            return _doctorService.GetPageAsync(page, filtrationModel, cancellationToken);
        }

        private async Task<DoctorDTO> DoctorToDoctorDTOConvertor(Doctor doctorData, CancellationToken cancellationToken)
        {
            var officeData = await _officeService.GetByIdAsync(doctorData.Office.Id, cancellationToken);

            var doctor = new DoctorDTO(doctorData, officeData);
            if (doctorData.Info.Photo != null)
            {
                doctor.Info.Photo = _imageService.GetAsync(doctorData.Info.Photo.Name, cancellationToken);
            }
            if (officeData.Photo != null)
            {
                doctor.Office.Photo = _imageService.GetAsync(officeData.Photo.Name, cancellationToken);
            }
            return doctor;
        }
    }
}
