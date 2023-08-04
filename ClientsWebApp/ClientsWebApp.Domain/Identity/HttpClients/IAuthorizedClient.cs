namespace ClientsWebApp.Domain.Identity.HttpClients
{
    public interface IAuthorizedClient
    {
        Uri? BaseAddress { get; }
        Task<HttpClient> GetHttpClientAsync();
    }
}
