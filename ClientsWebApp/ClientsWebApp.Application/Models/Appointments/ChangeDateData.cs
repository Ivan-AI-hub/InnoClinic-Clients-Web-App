﻿namespace ClientsWebApp.Application.Models.Appointments
{
    public class ChangeDateData
    {
        public DateTime Date { get; set; }
        public Guid AppointmentId { get; set; }
        public Guid DoctorId { get; set; }
    }
}
