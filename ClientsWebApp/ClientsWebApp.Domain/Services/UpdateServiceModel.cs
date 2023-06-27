namespace ClientsWebApp.Domain.Services
{
    public record UpdateServiceModel(string Name, int Price, bool Status, Guid SpecializationId, string CategoryName);
}
