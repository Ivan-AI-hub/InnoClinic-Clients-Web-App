﻿using ClientsWebApp.Application.Models.Offices;
using ClientsWebApp.Domain.Offices;
using ClientsWebApp.Domain.Profiles.Receptionist;

namespace ClientsWebApp.Application.Models.Receptionists
{
    public class ReceptionistDTO
    {
        public Guid Id { get; private set; }
        public HumanInfoDTO Info { get; private set; }
        public OfficeDTO Office { get; private set; }
        public ReceptionistDTO(Guid id, HumanInfoDTO info, OfficeDTO office)
        {
            Id = id;
            Info = info;
            Office = office;
        }
        public ReceptionistDTO(Receptionist receptionist, Office office)
        {
            Id = receptionist.Id;
            Info = new HumanInfoDTO(receptionist.Info);
            Office = new OfficeDTO(office);
        }
    }
}
