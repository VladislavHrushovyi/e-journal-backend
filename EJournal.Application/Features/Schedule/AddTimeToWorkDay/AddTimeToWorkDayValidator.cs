using FluentValidation;

namespace EJournal.Application.Features.Schedule.AddTimeToWorkDay;

public sealed class AddTimeToWorkDayValidator : AbstractValidator<AddTimeToWorkDayRequest>
{
    public AddTimeToWorkDayValidator()
    {
        RuleFor(src => src.Time)
            .NotEmpty()
            .NotNull()
            .Matches(@"\d{2}:\d{2}");
        RuleFor(src => src.DayOfWeek).IsInEnum();
    }
}