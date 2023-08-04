using ClientsWebApp.Domain.Appointments;
using System.Text;

namespace ClientsWebApp.Services.UriConstructors
{
    internal static class AppointmentUriConstructor
    {
        public static string GenerateFiltrationQuery(AppointmentFiltrationModel filtrationModel)
        {
            var quary = new StringBuilder("?");

            if (filtrationModel == null)
                return quary.ToString();

            if (!String.IsNullOrEmpty(filtrationModel.DoctorFirstName))
            {
                quary.Append($"&DoctorFirstName={filtrationModel.DoctorFirstName}");
            }
            if (!String.IsNullOrEmpty(filtrationModel.DoctorMiddleName))
            {
                quary.Append($"&DoctorMiddleName={filtrationModel.DoctorMiddleName}");
            }
            if (!String.IsNullOrEmpty(filtrationModel.DoctorLastName))
            {
                quary.Append($"&DoctorLastName={filtrationModel.DoctorLastName}");
            }
            if (!String.IsNullOrEmpty(filtrationModel.ServiceName))
            {
                quary.Append($"&ServiceName={filtrationModel.ServiceName}");
            }
            if (filtrationModel.Status != null)
            {
                quary.Append($"&Status={filtrationModel.Status}");
            }
            if (filtrationModel.DoctorId != default)
            {
                quary.Append($"&DoctorId={filtrationModel.DoctorId}");
            }
            if (filtrationModel.PatientId != default)
            {
                quary.Append($"&PatientId={filtrationModel.PatientId}");
            }
            if (filtrationModel.Date != default)
            {
                quary.Append($"&Date={filtrationModel.Date.ToString("yyyy-MM-dd")}");
            }

            return quary.ToString();
        }
    }
}
