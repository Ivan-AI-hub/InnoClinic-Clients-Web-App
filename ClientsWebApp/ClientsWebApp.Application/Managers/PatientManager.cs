using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Patients;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.PatientInfos;
using ClientsWebApp.Domain.Profiles;
using ClientsWebApp.Domain.Profiles.Patient;
using ClientsWebApp.Shared.Patient;

namespace ClientsWebApp.Application.Managers
{
    public class PatientManager : IPatientManager
    {
        private readonly IPatientInfoService _patientInfoService;
        private readonly IPatientService _patientService;
        private readonly IImageService _imageService;

        public PatientManager(IPatientService patientService, IImageService imageService, IPatientInfoService patientInfoService)
        {
            _patientService = patientService;
            _imageService = imageService;
            _patientInfoService = patientInfoService;
        }

        public async Task CreateAsync(CreatePatientData data, CancellationToken cancellationToken)
        {
            if (data.BirthDay is null)
            {
                return;
            }
            var info = new CreateHumanInfo(new ImageName(data.Picture.FileName), data.Email, data.FirstName, data.LastName, data.MiddleName, data.BirthDay.Value);
            var createModel = new CreatePatientModel(info, data.PhoneNumber);

            await _patientService.CreateAsync(createModel, cancellationToken);
            await _imageService.CreateAsync(data.Picture, cancellationToken);
        }

        public Task<PatientPersonalInfo> CreateInfoAsync(PatientPersonalInfo model, CancellationToken cancellationToken)
        {
            return _patientInfoService.CreateAsync(model, cancellationToken);
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _patientService.DeleteAsync(id, cancellationToken);
        }

        public Task DeleteInfoAsync(Guid patientId, CancellationToken cancellationToken)
        {
            return _patientInfoService.DeleteAsync(patientId, cancellationToken);
        }

        public async Task EditAsync(PatientDTO oldPatient, EditPatientData data, CancellationToken cancellationToken)
        {
            if (data.BirthDay is null)
            {
                return;
            }

            var fileName = (await oldPatient.Info.ToHumanInfo()).Photo;
            var imageName = data.Picture == null ? fileName : new ImageName(data.Picture.FileName);
            var updateModel = new UpdatePatientModel(imageName, data.FirstName, data.LastName, data.MiddleName, data.BirthDay.Value, data.PhoneNumber);

            await _patientService.UpdateAsync(oldPatient.Id, updateModel, cancellationToken);
            if (data.Picture != null && data.Picture != fileName)
            {
                try
                {
                    await _imageService.DeleteAsync(fileName.Name, cancellationToken);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    await _imageService.CreateAsync(data.Picture, cancellationToken);
                }
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

        public Task<PatientPersonalInfo> GetInfoForPatientAsync(Guid patientId, CancellationToken cancellationToken)
        {
            return _patientInfoService.GetForPatientAsync(patientId, cancellationToken);
        }

        public Task<IEnumerable<Patient>> GetInfoPageAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            return _patientService.GetPageAsync(page, filtrationModel, cancellationToken);
        }

        public async Task<IEnumerable<PatientDTO>> GetPageAsync(Page page, PatientFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            var patientsData = await _patientService.GetPageAsync(page, filtrationModel, cancellationToken);
            var patients = new List<PatientDTO>();
            foreach (var patient in patientsData)
            {
                patients.Add(await PatientToPatientDTOConvertor(patient, cancellationToken));
            }
            return patients;
        }

        public Task UpdateInfoAsync(Guid id, PatientPersonalInfo model, CancellationToken cancellationToken)
        {
            return _patientInfoService.UpdateAsync(id, model, cancellationToken);
        }

        private async Task<PatientDTO> PatientToPatientDTOConvertor(Patient patientData, CancellationToken cancellationToken)
        {
            var patient = new PatientDTO(patientData);
            if (patientData.Info.Photo != null)
            {
                patient.Info.Photo = _imageService.GetAsync(patientData.Info.Photo.Name, cancellationToken);
            }
            return patient;
        }
    }
}
