﻿using Application.Mediators.UserMediator.Add;
using FluentValidation;

namespace Application.Validators.UserValidators;

internal class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(u => u.Email).NotEmpty();
        RuleFor(u => u.Password).NotEmpty();
        RuleFor(u => u.PasswordConfirmation)
            .NotEmpty()
            .Equal(u => u.Password)
            .WithMessage("Password doesn't match");
    }
}