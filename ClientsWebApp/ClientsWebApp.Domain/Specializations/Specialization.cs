using ClientsWebApp.Domain.Services;

namespace ClientsWebApp.Domain.Specializations
{
    public class Specialization
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; set; }
        public List<Service> Services { get; private set; }

        public Specialization(Guid id, string name, bool isActive, List<Service> services)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            Services = services;
        }
    }
}
