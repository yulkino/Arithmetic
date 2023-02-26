using Application.Mediators.SettingsMediator.Get;
using FluentValidation;

namespace Application.Validators.SettingsValidators;

public class GetSettingsQueryValidator : AbstractValidator<GetSettingsQuery>
{
    public GetSettingsQueryValidator()
    {
        RuleFor(s => s.UserId).NotEmpty();
    }
}