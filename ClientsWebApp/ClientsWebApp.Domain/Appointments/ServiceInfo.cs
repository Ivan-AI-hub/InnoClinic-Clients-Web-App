namespace ClientsWebApp.Domain.Appointments
{
    public class ServiceInfo
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public ServiceInfo(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
