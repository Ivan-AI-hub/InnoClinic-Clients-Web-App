using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Profiles;
using ClientsWebApp.Domain.Profiles.Patient;

namespace ClientsWebApp.Application.Managers
{
    public class PatientManager : IPatientManager
    {
        private readonly IPatientService _patientService;
        private readonly IImageService _imageService;

        public PatientManager(IPatientService patientService, IImageService imageService)
        {
            _patientService = patientService;
            _imageService = imageService;
        }

        public async Task CreateAsync(CreatePatientData data, CancellationToken cancellationToken)
        {
            var info = new CreateHumanInfo(new ImageName(data.Picture.FileName), data.Email, data.FirstName, data.LastName, data.MiddleName, data.BirthDay);
            var createModel = new CreatePatientModel(info, data.PhoneNumber);

            await _patientService.CreateAsync(createModel, cancellationToken);
            await _imageService.CreateAsync(data.Picture, cancellationToken);
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _patientService.DeleteAsync(id, cancellationToken);
        }

        public async Task EditAsync(PatientDTO oldPatient, EditPatientData data, CancellationToken cancellationToken)
        {
            var imageName = data.Picture == null ? new ImageName(oldPatient.Info.Photo.FileName) : new ImageName(data.Picture.FileName);
            var updateModel = new UpdatePatientModel(imageName, data.FirstName, data.LastName, data.MiddleName, data.BirthDay, data.PhoneNumber);

            await _patientService.UpdateAsync(oldPatient.Id, updateModel, cancellationToken);
            if (data.Picture != null && data.Picture.FileName != oldPatient.Info.Photo?.FileName)
            {
                await _imageService.DeleteAsync(oldPatient.Info.Photo.FileName, cancellationToken);
                await _imageService.CreateAsync(data.Picture, cancellationToken);
            }
        }

        public async Task<PatientDTO> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var patientData = await _patientService.GetByEmailAsync(email, cancellationToken);
            return await PatientToPatientDTOConvertor(patientData, cancellationToken);
        }

        public async Task<PatientDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var patientData = await _patientService.GetByIdAsync(id, cancellationToken);
            return await PatientToPatientDTOConvertor(patientData, cancellationToken);
        }

        public Task<Patient> GetInfoByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return _patientService.GetByEmailAsync(email, cancellationToken);
        }

        public Task<IEnumerable<Patient>> GetInfoPageAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            return _patientService.GetPageAsync(page, filtrationModel, cancellationToken);
        }

        public async Task<IEnumerable<PatientDTO>> GetPageAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            var patients = await _patientService.GetPageAsync(page, filtrationModel, cancellationToken);
            return patients.Select(x => PatientToPatientDTOConvertor(x, cancellationToken).Result);
        }

        private async Task<PatientDTO> PatientToPatientDTOConvertor(Patient patientData, CancellationToken cancellationToken)
        {
            var patient = new PatientDTO(patientData);
            if (patientData.Info.Photo != null)
            {
                patient.Info.Photo = await _imageService.GetAsync(patientData.Info.Photo.Name, cancellationToken);
            }
            return patient;
        }
    }
}
