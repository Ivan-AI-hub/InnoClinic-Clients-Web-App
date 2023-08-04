using ClientsWebApp.Application.Models.Appointments;
using FluentValidation;

namespace ClientsWebApp.Application.Validators
{
    public class CreateAppointmentDataValidator : AbstractValidator<CreateAppointmentData>
    {
        public CreateAppointmentDataValidator()
        {
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Specialization).NotEmpty();
            RuleFor(x => x.PatientId).NotEmpty();
            RuleFor(x => x.DoctorId).NotEmpty();
            RuleFor(x => x.ServiceId).NotEmpty();
            RuleFor(x => x.OfficeId).NotEmpty();
            RuleFor(x => x.Date.ToDateTime(x.Time)).GreaterThanOrEqualTo(DateTime.Now);
        }
    }
}
