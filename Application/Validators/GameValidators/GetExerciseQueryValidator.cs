using Application.Mediators.GameMediator.GetExercise;
using FluentValidation;

namespace Application.Validators.GameValidators;

internal class GetExerciseQueryValidator : AbstractValidator<GetExerciseQuery>
{
    public GetExerciseQueryValidator()
    {
        RuleFor(e => e.UserId).NotEmpty();
        RuleFor(e => e.GameId).NotEmpty();
    }
}