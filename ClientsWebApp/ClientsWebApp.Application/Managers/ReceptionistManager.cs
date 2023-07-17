using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Receptionists;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles;
using ClientsWebApp.Domain.Profiles.Receptionist;

namespace ClientsWebApp.Application.Managers
{
    public class ReceptionistManager : IReceptionistManager
    {
        private readonly IReceptionistService _receptionistService;
        private readonly IImageService _imageService;
        private readonly IOfficeService _officeService;

        public ReceptionistManager(IReceptionistService receptionistService, IImageService imageService, IOfficeService officeService)
        {
            _receptionistService = receptionistService;
            _imageService = imageService;
            _officeService = officeService;
        }

        public async Task CreateAsync(CreateReceptionistData data, CancellationToken cancellationToken)
        {
            var info = new CreateHumanInfo(new ImageName(data.Picture.FileName), data.Email, data.FirstName, data.LastName, data.MiddleName, data.BirthDay);
            var createModel = new CreateReceptionistModel(info, new OfficeId(data.OfficeId));

            await _receptionistService.CreateAsync(createModel, cancellationToken);
            await _imageService.CreateAsync(data.Picture, cancellationToken);
        }

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            return _receptionistService.DeleteAsync(id, cancellationToken);
        }

        public async Task EditAsync(ReceptionistDTO oldReceptionist, EditReceptionistData data, CancellationToken cancellationToken)
        {
            var imageName = data.Picture == null ? new ImageName(oldReceptionist.Info.Photo.FileName) : new ImageName(data.Picture.FileName);
            var updateModel = new UpdateReceptionistModel(imageName, data.FirstName, data.LastName, data.MiddleName, data.BirthDay, data.OfficeId);

            await _receptionistService.UpdateAsync(oldReceptionist.Id, updateModel, cancellationToken);
            if (data.Picture != null && data.Picture.FileName != oldReceptionist.Info.Photo?.FileName)
            {
                await _imageService.DeleteAsync(oldReceptionist.Info.Photo.FileName, cancellationToken);
                await _imageService.CreateAsync(data.Picture, cancellationToken);
            }
        }

        public async Task<ReceptionistDTO> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var receptionistData = await _receptionistService.GetByEmailAsync(email, cancellationToken);
            return await ReceptionistToReceptionistDTOConvertor(receptionistData, cancellationToken);
        }

        public async Task<ReceptionistDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var receptionistData = await _receptionistService.GetByIdAsync(id, cancellationToken);
            return await ReceptionistToReceptionistDTOConvertor(receptionistData, cancellationToken);
        }

        public Task<IEnumerable<Receptionist>> GetInfoPageAsync(Page page, ReceptionistFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            return _receptionistService.GetPageAsync(page, filtrationModel, cancellationToken);
        }

        public async Task<IEnumerable<ReceptionistDTO>> GetPageAsync(Page page, ReceptionistFiltrationModel filtrationModel, CancellationToken cancellationToken)
        {
            var receptionists = await _receptionistService.GetPageAsync(page, filtrationModel, cancellationToken);
            return receptionists.Select(x => ReceptionistToReceptionistDTOConvertor(x, cancellationToken).Result);

        }
        private async Task<ReceptionistDTO> ReceptionistToReceptionistDTOConvertor(Receptionist receptionistData, CancellationToken cancellationToken)
        {
            var officeData = await _officeService.GetByIdAsync(receptionistData.Office.Id, cancellationToken);

            var receptionist = new ReceptionistDTO(receptionistData, officeData);
            if (receptionistData.Info.Photo != null)
            {
                receptionist.Info.Photo = await _imageService.GetAsync(receptionistData.Info.Photo.Name, cancellationToken);
            }
            if (officeData.Photo != null)
            {
                receptionist.Office.Photo = await _imageService.GetAsync(officeData.Photo.Name, cancellationToken);
            }
            return receptionist;
        }
    }
}
