using ClientsWebApp.Application.Models.Services;
using FluentValidation;

namespace ClientsWebApp.Application.Validators
{
    public class CreateServiceDataValidator : AbstractValidator<CreateServiceData>
    {
        public CreateServiceDataValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.CategoryName).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}
