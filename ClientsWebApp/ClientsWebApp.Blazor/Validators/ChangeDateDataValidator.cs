using ClientsWebApp.Blazor.Pages.Appointments.Models;
using FluentValidation;

namespace ClientsWebApp.Blazor.Validators
{
    public class ChangeDateDataValidator : AbstractValidator<ChangeDateData>
    {
        public ChangeDateDataValidator()
        {
            RuleFor(x => x.Date).GreaterThan(DateTime.Now);
        }
    }
}
