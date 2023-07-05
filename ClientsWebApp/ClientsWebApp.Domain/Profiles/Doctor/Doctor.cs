namespace ClientsWebApp.Domain.Profiles.Doctor
{
    public class Doctor
    {
        public Guid Id { get; private set; }
        public HumanInfo Info { get; private set; }
        public string Specialization { get; private set; }
        public OfficeId Office { get; private set; }
        public int CareerStartYear { get; private set; }
        public WorkStatus Status { get; private set; }
        public int Experience => DateTime.UtcNow.Year - CareerStartYear + 1;
        public Doctor(Guid id, HumanInfo info, string specialization, OfficeId office, int careerStartYear, WorkStatus status)
        {
            Id = id;
            Info = info;
            Specialization = specialization;
            Office = office;
            CareerStartYear = careerStartYear;
            Status = status;
        }
    }
}
