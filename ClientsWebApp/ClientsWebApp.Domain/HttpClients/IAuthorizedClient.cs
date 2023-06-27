namespace ClientsWebApp.Domain.HttpClients
{
    public interface IAuthorizedClient
    {
        Uri? BaseAddress { get; }
        Task<HttpClient> GetHttpClientAsync();
    }
}
