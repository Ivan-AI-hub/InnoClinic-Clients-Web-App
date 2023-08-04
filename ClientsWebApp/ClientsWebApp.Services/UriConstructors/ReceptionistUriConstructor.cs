﻿using ClientsWebApp.Domain.Profiles.Receptionist;
using System.Text;

namespace ClientsWebApp.Services.UriConstructors
{
    internal static class ReceptionistUriConstructor
    {
        public static string GenerateFiltrationQuery(ReceptionistFiltrationModel filtrationModel)
        {
            var quary = new StringBuilder("?");

            if (filtrationModel == null)
                return quary.ToString();

            if (!String.IsNullOrEmpty(filtrationModel.FirstName))
            {
                quary.Append($"&FirstName={filtrationModel.FirstName}");
            }
            if (!String.IsNullOrEmpty(filtrationModel.MiddleName))
            {
                quary.Append($"&MiddleName={filtrationModel.MiddleName}");
            }
            if (!String.IsNullOrEmpty(filtrationModel.LastName))
            {
                quary.Append($"&LastName={filtrationModel.LastName}");
            }

            return quary.ToString();
        }
    }
}
