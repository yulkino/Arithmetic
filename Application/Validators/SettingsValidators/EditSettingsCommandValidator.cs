using Application.Mediators.SettingsMediator.Edit;
using FluentValidation;

namespace Application.Validators.SettingsValidators;

public class EditSettingsCommandValidator : AbstractValidator<EditSettingsCommand>
{
    public EditSettingsCommandValidator()
    {
        RuleFor(s => s.UserId)
            .NotEmpty();
        RuleFor(s => s.ExerciseCount)
            .NotEmpty();
        RuleFor(s => s.DifficultyId)
            .NotEmpty();
        RuleFor(s => s.OperationIds)
            .NotEmpty();
        RuleForEach(s => s.OperationIds)
            .NotEmpty();
    }
}