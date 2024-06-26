﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientsWebApp.Shared.Patient.Models
{
    public class WomanPregnancy
    {
        public WomanPregnancy(DateTime? discoveryDate, DateTime? endDate, string result)
        {
            DiscoveryDate = discoveryDate;
            EndDate = endDate;
            Result = result;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime? DiscoveryDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Result { get; set; }
    }
}
