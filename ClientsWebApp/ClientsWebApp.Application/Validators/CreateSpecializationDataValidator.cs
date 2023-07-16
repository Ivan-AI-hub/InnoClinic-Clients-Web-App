using ClientsWebApp.Application.Models.Specializations;
using FluentValidation;

namespace ClientsWebApp.Application.Validators
{
    public class CreateSpecializationDataValidator : AbstractValidator<CreateSpecializationData>
    {
        public CreateSpecializationDataValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
