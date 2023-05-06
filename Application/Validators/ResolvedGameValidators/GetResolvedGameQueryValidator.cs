using Application.Mediators.ResolvedGameMediator.Get;
using FluentValidation;

namespace Application.Validators.ResolvedGameValidators;

internal class GetResolvedGameQueryValidator : AbstractValidator<GetResolvedGameQuery>
{
    public GetResolvedGameQueryValidator()
    {
        RuleFor(r => r.UserId).NotEmpty();
        RuleFor(r => r.GameId).NotEmpty();
    }
}