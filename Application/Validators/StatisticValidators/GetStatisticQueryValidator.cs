using Application.Mediators.StatisticMediator.Get;
using FluentValidation;

namespace Application.Validators.StatisticValidators;

internal class GetStatisticQueryValidator : AbstractValidator<GetStatisticQuery>
{
    public GetStatisticQueryValidator() => RuleFor(s => s.UserId).NotEmpty();
}