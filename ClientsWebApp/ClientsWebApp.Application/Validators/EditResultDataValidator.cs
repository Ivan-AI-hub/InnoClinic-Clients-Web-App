using ClientsWebApp.Application.Models.Results;
using FluentValidation;

namespace ClientsWebApp.Application.Validators
{
    public class EditResultDataValidator : AbstractValidator<EditResultData>
    {
        public EditResultDataValidator()
        {
            RuleFor(x => x.Conclusion).NotEmpty();
            RuleFor(x => x.Complaints).NotEmpty();
            RuleFor(x => x.Recomendations).NotEmpty();
        }
    }
}
