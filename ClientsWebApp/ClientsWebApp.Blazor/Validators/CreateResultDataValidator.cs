using ClientsWebApp.Blazor.Pages.Results.Models;
using FluentValidation;

namespace ClientsWebApp.Blazor.Validators
{
    public class CreateResultDataValidator : AbstractValidator<CreateResultData>
    {
        public CreateResultDataValidator()
        {
            RuleFor(x => x.Conclusion).NotEmpty();
            RuleFor(x => x.Complaints).NotEmpty();
            RuleFor(x => x.Recomendations).NotEmpty();
            RuleFor(x => x.PatientId).NotEmpty();
            RuleFor(x => x.DoctorId).NotEmpty();
            RuleFor(x => x.AppointmentId).NotEmpty();
        }

    }
}
