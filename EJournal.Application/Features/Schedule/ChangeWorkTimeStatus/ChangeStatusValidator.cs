using System.Data;
using FluentValidation;

namespace EJournal.Application.Features.Schedule.ChangeWorkTimeStatus;

public sealed class ChangeStatusValidator : AbstractValidator<ChangeStatusRequest>
{
    public ChangeStatusValidator()
    {
        RuleFor(src => src.DayOfWeek)
            .IsInEnum();
        RuleFor(src => src.Status)
            .IsInEnum();
        RuleFor(src => src.WorkTimeId)
            .NotEmpty()
            .NotNull();
    }
}