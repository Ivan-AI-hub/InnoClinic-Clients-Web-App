using ClientsWebApp.Domain.Offices;

namespace ClientsWebApp.Domain.Profiles.Receptionist
{
    public class Receptionist
    {
        public Guid Id { get; private set; }
        public HumanInfo Info { get; private set; }
        public OfficeId Office { get; private set; }
        public Receptionist(Guid id, HumanInfo info, OfficeId office)
        {
            Id = id;
            Info = info;
            Office = office;
        }
    }
}
