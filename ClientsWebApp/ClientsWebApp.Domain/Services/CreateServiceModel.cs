namespace ClientsWebApp.Domain.Services
{
    public record CreateServiceModel(string Name, int Price, bool Status, Guid SpecializationId, string CategoryName);
}
