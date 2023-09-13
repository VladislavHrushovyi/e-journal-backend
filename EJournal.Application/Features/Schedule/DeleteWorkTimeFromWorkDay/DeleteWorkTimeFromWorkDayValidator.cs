using FluentValidation;

namespace EJournal.Application.Features.Schedule.DeleteWorkTimeFromWorkDay;

public sealed class DeleteWorkTimeFromWorkDayValidator : AbstractValidator<DeleteWorkTimeFromWorkDayRequest>
{
    public DeleteWorkTimeFromWorkDayValidator()
    {
        RuleFor(src => src.WorkTimeId)
            .NotEmpty()
            .NotNull();
        RuleFor(src => src.DayOfWeek)
            .IsInEnum();
    }
}