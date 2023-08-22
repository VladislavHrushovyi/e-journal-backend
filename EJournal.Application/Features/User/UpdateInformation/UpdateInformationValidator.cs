using FluentValidation;

namespace EJournal.Application.Features.User.UpdateInformation;

public class UpdateInformationValidator : AbstractValidator<UpdateInformationRequest>
{
    public UpdateInformationValidator()
    {
        RuleFor(src => src.PhoneNumber)
            .NotNull()
            .NotEmpty()
            .Matches(@"(^\+38)\(?(\d{3})\)?(\d{3})\-?(\d{2})\-?(\d{2})");
        RuleFor(src => src.Surname)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3);
        RuleFor(src => src.FirstName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3);
    }
}