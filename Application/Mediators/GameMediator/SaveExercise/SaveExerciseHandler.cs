using Domain.Entity.Exercises;
using ErrorOr;
using MediatR;

namespace Application.Mediators.GameMediator.SaveExercise;

public class SaveExerciseHandler : IRequestHandler<SaveExerciseCommand, ErrorOr<Exercise>>
{
    public Task<ErrorOr<Exercise>> Handle(SaveExerciseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}