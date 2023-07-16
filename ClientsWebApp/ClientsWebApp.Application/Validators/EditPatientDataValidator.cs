using ClientsWebApp.Application.Models.Patients;
using FluentValidation;

namespace ClientsWebApp.Application.Validators
{
    public class EditPatientDataValidator : AbstractValidator<EditPatientData>
    {
        public EditPatientDataValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.MiddleName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.BirthDay).NotEmpty();
        }
    }
}
