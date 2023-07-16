using ClientsWebApp.Application.Models.Doctors;
using FluentValidation;

namespace ClientsWebApp.Application.Validators
{
    public class CreateDoctorDataValidator : AbstractValidator<CreateDoctorData>
    {
        public CreateDoctorDataValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.MiddleName).NotEmpty();
            RuleFor(x => x.BirthDay).NotEmpty();
            RuleFor(x => x.Specialization).NotEmpty();
            RuleFor(x => x.OfficeId).NotEmpty();
            RuleFor(x => x.CareerStartYear).NotEmpty();
        }
    }
}
