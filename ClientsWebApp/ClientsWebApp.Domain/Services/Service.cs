using ClientsWebApp.Domain.Categories;
using ClientsWebApp.Domain.Specializations;

namespace ClientsWebApp.Domain.Services
{
    public class Service
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int Price { get; private set; }
        public bool Status { get; private set; }
        public Guid SpecializationId { get; private set; }
        public Guid CategoryId { get; private set; }
        public Specialization? Specialization { get; private set; }
        public Category? Category { get; private set; }
        public Service(Guid id, string name, int price, bool status, Specialization specialization, Category category, Guid specializationId, Guid categoryId)
        {
            Id = id;
            Name = name;
            Price = price;
            Status = status;
            Specialization = specialization;
            Category = category;
            SpecializationId = specializationId;
            CategoryId = categoryId;
        }
    }
}
