using ClientsWebApp.Blazor.Pages.Offices.Models;
using FluentValidation;

namespace ClientsWebApp.Blazor.Validators
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
