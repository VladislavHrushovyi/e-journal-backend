using FluentValidation;

namespace EJournal.Application.Features.User.RecordingToTimeWork;

public sealed class RecordingToTimeWorkValidator : AbstractValidator<RecordingToTimeWorkRequest>
{
    public RecordingToTimeWorkValidator()
    {
        RuleFor(src => src.DayOfWeek)
            .NotEmpty()
            .NotEmpty()
            .IsInEnum();
        RuleFor(src => src.TimeId)
            .NotEmpty()
            .NotNull();
        RuleFor(src => src.UserId)
            .NotEmpty()
            .NotNull();
        RuleFor(src => src.UserMessage).MaximumLength(200);
    }
}