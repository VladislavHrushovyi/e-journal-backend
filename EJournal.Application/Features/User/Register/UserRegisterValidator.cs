﻿using FluentValidation;

namespace EJournal.Application.Features.User.Register;

public sealed class UserRegisterValidator : AbstractValidator<UserRegisterRequest>
{
    public UserRegisterValidator()
    {
        RuleFor(src => src.FirstName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(25);
        RuleFor(src => src.LastName)
            .NotEmpty()
            .NotNull()
            .MinimumLength(3)
            .MaximumLength(25);
        RuleFor(src => src.Password)
            .NotNull()
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(25);
        RuleFor(src => src.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .Length(13)
            .Matches(@"(^\+38)\(?(\d{3})\)?(\d{3})\-?(\d{2})\-?(\d{2})");
    }
}