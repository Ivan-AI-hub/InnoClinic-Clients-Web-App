using ClientsWebApp.Blazor.Pages.Results.Models;
using FluentValidation;

namespace ClientsWebApp.Blazor.Validators
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
