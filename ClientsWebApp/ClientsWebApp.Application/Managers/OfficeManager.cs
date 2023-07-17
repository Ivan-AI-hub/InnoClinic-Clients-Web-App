using ClientsWebApp.Application.Abstraction;
using ClientsWebApp.Application.Models.Offices;
using ClientsWebApp.Domain;
using ClientsWebApp.Domain.Images;
using ClientsWebApp.Domain.Offices;

namespace ClientsWebApp.Application.Managers
{
    public class OfficeManager : IOfficeManager
    {
        private readonly IOfficeService _officeService;
        private readonly IImageService _imageService;

        public OfficeManager(IOfficeService officeService, IImageService imageService)
        {
            _officeService = officeService;
            _imageService = imageService;
        }

        public async Task CreateAsync(CreateOfficeData data, CancellationToken cancellationToken)
        {
            var createModel = new CreateOfficeModel(new ImageName(data.Picture.FileName), data.City, data.Street, data.HouseNumber, data.OfficeNumber, data.PhoneNumber, data.Status);

            await _officeService.CreateAsync(createModel, cancellationToken);
            await _imageService.CreateAsync(data.Picture, cancellationToken);

        }
        public async Task EditAsync(OfficeDTO oldOffice, EditOfficeData data, CancellationToken cancellationToken)
        {
            var imageName = data.Picture == null ? new ImageName(oldOffice.Photo.FileName) : new ImageName(data.Picture.FileName);
            var updateModel = new UpdateOfficeModel(imageName, data.City, data.Street, data.HouseNumber, data.OfficeNumber, data.PhoneNumber, data.Status);

            await _officeService.UpdateAsync(oldOffice.Id, updateModel, cancellationToken);
            if (data.Picture != null && data.Picture.FileName != oldOffice.Photo.FileName)
            {
                await _imageService.DeleteAsync(oldOffice.Photo.FileName, cancellationToken);
                await _imageService.CreateAsync(data.Picture, cancellationToken);
            }

        }
        public async Task UpdateStatusAsync(Guid id, bool newStatus, CancellationToken cancellationToken)
        {
            var model = new UpdateOfficeStatusModel(newStatus);
            await _officeService.UpdateStatusAsync(id, model, cancellationToken);
        }

        public async Task<OfficeDTO> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var officeData = await _officeService.GetByIdAsync(id, cancellationToken);
            return await OfficeToOfficeDTOConvertor(officeData, cancellationToken);
        }

        public Task<IEnumerable<Office>> GetInfoPageAsync(Page page, CancellationToken cancellationToken)
        {
            return _officeService.GetPageAsync(page, cancellationToken);
        }

        public async Task<IEnumerable<OfficeDTO>> GetPageAsync(Page page, CancellationToken cancellationToken)
        {
            var offices = await _officeService.GetPageAsync(page, cancellationToken);
            return offices.Select(x => OfficeToOfficeDTOConvertor(x, cancellationToken).Result);
        }

        private async Task<OfficeDTO> OfficeToOfficeDTOConvertor(Office officeData, CancellationToken cancellationToken)
        {
            var office = new OfficeDTO(officeData);
            if (officeData.Photo != null)
            {
                office.Photo = await _imageService.GetAsync(officeData.Photo.Name, cancellationToken);
            }
            return office;
        }
    }
}
