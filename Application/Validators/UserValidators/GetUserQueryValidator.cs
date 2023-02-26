using Application.Mediators.UserMediator.Get;
using FluentValidation;

namespace Application.Validators.UserValidators;
internal class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(u => u.Login).NotEmpty();
        RuleFor(u => u.Password).NotEmpty();
    }
}
