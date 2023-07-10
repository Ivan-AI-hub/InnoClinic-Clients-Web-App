using ClientsWebApp.Blazor.Pages.Appointments.Models;
using FluentValidation;

namespace ClientsWebApp.Blazor.Validators
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
            RuleFor(x => x.Date).GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));
            RuleFor(x => x.Time).GreaterThanOrEqualTo(TimeOnly.FromDateTime(DateTime.Now));
        }
    }
}
