using FluentValidation;

namespace EJournal.Application.Features.Schedule.UpdateIsWorkDay;

public class UpdateIsWorkDayValidator : AbstractValidator<UpdateIsWorkDayRequest>
{
    public UpdateIsWorkDayValidator()
    {
        RuleFor(src => src.WorkDayId)
            .NotNull()
            .NotEmpty();
        RuleFor(src => src.IsWorkDay)
            .NotNull();
    }
}