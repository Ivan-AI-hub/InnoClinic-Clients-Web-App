﻿namespace ClientsWebApp.Shared.Patient.Models
{
    public class BodyFeature
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public BodyFeature(string name)
        {
            Name = name;
        }
    }
}
