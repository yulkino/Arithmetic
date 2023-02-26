using Domain.Entity.ExerciseEntities;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Application.Mediators.GameMediator.SaveExercise;

public class SaveExerciseHandler : IRequestHandler<SaveExerciseCommand, ErrorOr<Exercise>>
{
    private readonly IValidator<SaveExerciseCommand> _validator;

    public SaveExerciseHandler(IValidator<SaveExerciseCommand> validator)
    {
        _validator = validator;
    }

    public Task<ErrorOr<Exercise>> Handle(SaveExerciseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}