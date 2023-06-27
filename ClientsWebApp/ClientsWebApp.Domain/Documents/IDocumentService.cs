using Microsoft.AspNetCore.Http;

namespace ClientsWebApp.Domain.Documents
{
    public interface IDocumentService
    {
        public Task CreateAsync(IFormFile file, CancellationToken cancellationToken);
        public Task DeleteAsync(string documentName, CancellationToken cancellationToken);
        public Task<Document> GetAsync(string documentName, CancellationToken cancellationToken);
    }
}
