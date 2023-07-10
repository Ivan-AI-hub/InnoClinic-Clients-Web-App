﻿namespace ClientsWebApp.Blazor.Pages.Appointments.Models
{
    public class TimeSlotsData
    {
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
