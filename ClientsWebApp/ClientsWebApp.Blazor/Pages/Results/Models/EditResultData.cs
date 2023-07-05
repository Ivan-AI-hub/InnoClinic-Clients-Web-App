using ClientsWebApp.Domain.Results;

namespace ClientsWebApp.Blazor.Pages.Results.Models
{
    public class EditResultData
    {
        public string Complaints { get; set; }
        public string Conclusion { get; set; }
        public string Recomendations { get; set; }

        public EditResultData(AppointmentResult result)
        {
            Complaints = result.Complaints;
            Conclusion = result.Conclusion;
            Recomendations = result.Recomendations;
        }
    }
}
