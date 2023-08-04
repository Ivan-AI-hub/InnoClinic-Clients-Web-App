using ClientsWebApp.Application.Models.Patients;
using FluentValidation;

namespace ClientsWebApp.Application.Validators
{
    public class CreatePatientDataValidator : AbstractValidator<CreatePatientData>
    {
        public CreatePatientDataValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.MiddleName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.BirthDay).NotEmpty();
        }
    }
}
