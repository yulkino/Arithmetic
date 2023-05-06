using Application.Mediators.GameMediator.Add;
using FluentValidation;

namespace Application.Validators.GameValidators;

internal class AddGameCommandValidator : AbstractValidator<AddGameCommand>
{
    public AddGameCommandValidator() => RuleFor(g => g.UserId).NotEmpty();
}