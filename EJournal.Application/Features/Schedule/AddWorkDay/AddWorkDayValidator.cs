using FluentValidation;

namespace EJournal.Application.Features.Schedule.AddWorkDay;

public sealed class AddWorkDayValidator : AbstractValidator<AddWorkDayRequest>
{
    public AddWorkDayValidator()
    {
        RuleFor(src => src.IsWorkDay)
            .NotNull();
        RuleFor(src => src.DayOfWeek)
            .IsInEnum();
    }
}