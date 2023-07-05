using ClientsWebApp.Blazor.Pages.Services.Models;
using FluentValidation;

namespace ClientsWebApp.Blazor.Validators
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
