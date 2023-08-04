using ClientsWebApp.Application.Models.Offices;
using FluentValidation;

namespace ClientsWebApp.Application.Validators
{
    public class CreateOfficeDataValidator : AbstractValidator<CreateOfficeData>
    {
        public CreateOfficeDataValidator()
        {
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Street).NotEmpty();
            RuleFor(x => x.HouseNumber).NotEmpty();
            RuleFor(x => x.OfficeNumber).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}
