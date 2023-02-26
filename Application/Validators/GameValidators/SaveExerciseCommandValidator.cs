using Application.Mediators.GameMediator.SaveExercise;
using FluentValidation;

namespace Application.Validators.GameValidators;

public class SaveExerciseCommandValidator : AbstractValidator<SaveExerciseCommand>
{
    public SaveExerciseCommandValidator()
    {
        RuleFor(e => e.UserId).NotEmpty();
        RuleFor(e => e.GameId).NotEmpty();
        RuleFor(e => e.Answer).NotNull();
    }
}