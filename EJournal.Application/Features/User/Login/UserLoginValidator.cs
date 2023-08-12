using FluentValidation;

namespace EJournal.Application.Features.User.Login;

public sealed class UserLoginValidator : AbstractValidator<UserLoginRequest>
{
    public UserLoginValidator()
    {
        RuleFor(src => src.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .Length(13)
            .Matches(@"(^\+38)\(?(\d{3})\)?(\d{3})\-?(\d{2})\-?(\d{2})");
        RuleFor(src => src.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(25);
    }
}