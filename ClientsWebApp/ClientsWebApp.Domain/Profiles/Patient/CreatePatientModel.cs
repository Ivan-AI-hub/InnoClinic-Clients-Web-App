namespace ClientsWebApp.Domain.Profiles.Patient
{
    public record CreatePatientModel(CreateHumanInfo Info)
    {
        public string PhoneNumber { get; set; } = "";
    }
}
