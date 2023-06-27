using ClientsWebApp.Domain.Services;

namespace ClientsWebApp.Domain.Categories
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public int TimeSlotSize { get; private set; }
        public List<Service> Services { get; private set; }

        public Category(Guid id, string name, int timeSlotSize, List<Service> services)
        {
            Id = id;
            Name = name;
            TimeSlotSize = timeSlotSize;
            Services = services;
        }
    }
}
