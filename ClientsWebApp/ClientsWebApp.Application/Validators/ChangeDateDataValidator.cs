using ClientsWebApp.Application.Models.Appointments;
using FluentValidation;

namespace ClientsWebApp.Application.Validators
{
    public class ChangeDateDataValidator : AbstractValidator<ChangeDateData>
    {
        public ChangeDateDataValidator()
        {
            RuleFor(x => x.Date).GreaterThan(DateTime.Now);
        }
    }
}
