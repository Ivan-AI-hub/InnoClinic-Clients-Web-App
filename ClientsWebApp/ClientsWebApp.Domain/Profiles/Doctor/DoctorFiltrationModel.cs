﻿namespace ClientsWebApp.Domain.Profiles.Doctor
{
    public class DoctorFiltrationModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Specialization { get; set; } = default;
        public Guid OfficeId { get; set; } = default;
    }
}
