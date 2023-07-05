using ClientsWebApp.Blazor.Pages.Specializations.Models;
using FluentValidation;

namespace ClientsWebApp.Blazor.Validators
{
    public class CreateSpecializationDataValidator : AbstractValidator<CreateSpecializationData>
    {
        public CreateSpecializationDataValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
