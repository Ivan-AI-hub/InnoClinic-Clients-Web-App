using ClientsWebApp.Blazor.Pages.Appointments;
using FluentValidation;
using ClientsWebApp.Blazor.Pages.Appointments.Models;

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
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.Time).NotEmpty();
        }
    }
}
