using ClientsWebApp.Domain.Profiles.Doctor;
using System.Text;

namespace ClientsWebApp.Services.UriConstructors
{
    internal static class DoctorUriConstructor
    {
        public static string GenerateFiltrationQuery(DoctorFiltrationModel filtrationModel)
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
            if (!String.IsNullOrEmpty(filtrationModel.Specialization))
            {
                quary.Append($"&Specialization={filtrationModel.Specialization}");
            }
            if (filtrationModel.OfficeId != default)
            {
                quary.Append($"&OfficeId={filtrationModel.OfficeId}");
            }

            return quary.ToString();
        }
    }
}
