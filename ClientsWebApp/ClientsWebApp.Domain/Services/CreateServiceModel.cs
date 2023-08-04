namespace ClientsWebApp.Domain.Services
{
    public class CreateServiceModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public bool Status { get; set; }
        public Guid SpecializationId { get; set; }
        public string CategoryName { get; set; }
        public CreateServiceModel(string name, int price, bool status, Guid specializationId, string categoryName)
        {
            Name = name;
            Price = price;
            Status = status;
            SpecializationId = specializationId;
            CategoryName = categoryName;
        }
    }
}
