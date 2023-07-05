using ClientsWebApp.Blazor.Pages.Profiles.Receptionists.Models;
using FluentValidation;

namespace ClientsWebApp.Blazor.Validators
{
    public class CreateReceptionistDataValidator : AbstractValidator<CreateReceptionistData>
    {
        public CreateReceptionistDataValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.MiddleName).NotEmpty();
            RuleFor(x => x.BirthDay).NotEmpty();
            RuleFor(x => x.OfficeId).NotEmpty();
        }
    }
}
