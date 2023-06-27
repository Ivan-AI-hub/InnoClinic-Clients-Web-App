namespace ClientsWebApp.Domain.Profiles.Doctor
{
    public record CreateDoctorModel(CreateHumanInfo Info)
    {
        public string Specialization { get; set; }
        public Guid OfficeId { get; set; }
        public int CareerStartYear { get; set; }
    }
}
