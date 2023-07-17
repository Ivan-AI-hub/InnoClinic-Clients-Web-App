namespace ClientsWebApp.Application.Models.Services
{
    public class EditServiceData
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
        public string CategoryName { get; set; }
        public Guid SpecializationId { get; set; }
    }
}
