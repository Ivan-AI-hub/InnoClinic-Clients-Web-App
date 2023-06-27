using Microsoft.AspNetCore.Http;

namespace ClientsWebApp.Domain.Images
{
    public interface IImageService
    {
        public Task CreateAsync(IFormFile file, CancellationToken cancellationToken);
        public Task DeleteAsync(string imageName, CancellationToken cancellationToken);
        public Task GetAsync(string imageName, CancellationToken cancellationToken);
    }
}
