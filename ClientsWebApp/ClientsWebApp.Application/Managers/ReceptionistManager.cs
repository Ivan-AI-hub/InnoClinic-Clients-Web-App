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
            if(data.BirthDay is null)
            {
                return;
            }

            var info = new CreateHumanInfo(new ImageName(data.Picture.FileName), data.Email, data.FirstName, data.LastName, data.MiddleName, data.BirthDay.Value);
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
            if(data.BirthDay is null)
            {
                return;
            }    

            var fileName = (await oldReceptionist.Info.ToHumanInfo()).Photo.Name; 
            var imageName = data.Picture == null ? new ImageName(fileName) : new ImageName(data.Picture.FileName);
            var updateModel = new UpdateReceptionistModel(imageName, data.FirstName, data.LastName, data.MiddleName, data.BirthDay.Value, data.OfficeId);

            await _receptionistService.UpdateAsync(oldReceptionist.Id, updateModel, cancellationToken);
            if (data.Picture != null && data.Picture.FileName != fileName)
            {
                await _imageService.DeleteAsync(fileName, cancellationToken);
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
            var receptionistsData = await _receptionistService.GetPageAsync(page, filtrationModel, cancellationToken);
            var receptionists = new List<ReceptionistDTO>();
            foreach (var receptionist in receptionistsData)
            {
                receptionists.Add(await ReceptionistToReceptionistDTOConvertor(receptionist, cancellationToken));
            }
            return receptionists;

        }
        private async Task<ReceptionistDTO> ReceptionistToReceptionistDTOConvertor(Receptionist receptionistData, CancellationToken cancellationToken)
        {
            var officeData = await _officeService.GetByIdAsync(receptionistData.Office.Id, cancellationToken);

            var receptionist = new ReceptionistDTO(receptionistData, officeData);

            if (receptionistData.Info.Photo != null)
            {
                receptionist.Info.Photo = _imageService.GetAsync(receptionistData.Info.Photo.Name, cancellationToken);
            }
            if (officeData.Photo != null)
            {
                receptionist.Office.Photo = _imageService.GetAsync(officeData.Photo.Name, cancellationToken);
            }
            
            return receptionist;
        }
    }
}
