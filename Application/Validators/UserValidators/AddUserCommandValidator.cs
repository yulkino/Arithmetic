using Application.Mediators.UserMediator.Add;
using FluentValidation;

namespace Application.Validators.UserValidators;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(u => u.Login).NotEmpty();
        RuleFor(u => u.Password).NotEmpty();
        RuleFor(u => u.PasswordConfirmation)
            .NotEmpty()
            .Equal(u => u.Password);
    }
}