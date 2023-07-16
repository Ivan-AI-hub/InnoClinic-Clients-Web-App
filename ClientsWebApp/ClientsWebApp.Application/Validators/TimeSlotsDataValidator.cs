using ClientsWebApp.Application.Models.Appointments;
using FluentValidation;

namespace ClientsWebApp.Application.Validators
{
    public class TimeSlotsDataValidator : AbstractValidator<TimeSlotsData>
    {
        public TimeSlotsDataValidator()
        {
            RuleFor(x => x.Date).GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now));
            RuleFor(x => x.StartTime).GreaterThanOrEqualTo(TimeOnly.FromDateTime(DateTime.Now));
            RuleFor(x => x.EndTime).NotEmpty();
        }
    }
}
