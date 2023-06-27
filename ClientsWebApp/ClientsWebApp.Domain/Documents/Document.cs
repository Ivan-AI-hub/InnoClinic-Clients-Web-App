namespace ClientsWebApp.Domain.Documents
{
    public record Document(string FileName, string ContentType, byte[] Content);
}
