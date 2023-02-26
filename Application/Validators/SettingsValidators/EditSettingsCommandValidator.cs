using Application.Mediators.SettingsMediator.Edit;
using FluentValidation;

namespace Application.Validators.SettingsValidators;

public class EditSettingsCommandValidator : AbstractValidator<EditSettingsCommand>
{
    public EditSettingsCommandValidator()
    {
        RuleFor(s => s.UserId).NotEmpty();
        RuleFor(s => s.ExerciseCount).NotEmpty();
        RuleFor(s => s.Difficulty.Id).NotEmpty();
        RuleFor(s => s.Operations).NotEmpty();
        RuleForEach(s => s.Operations).NotEmpty();
    }
}