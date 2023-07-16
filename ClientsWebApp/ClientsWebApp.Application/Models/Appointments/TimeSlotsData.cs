namespace ClientsWebApp.Application.Models.Appointments
{
    public class TimeSlotsData
    {
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
