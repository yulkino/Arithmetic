using Domain.Entity.Exercises;

namespace Application.Mediators.GameMediator.SaveExercise;

public record SaveExerciseCommand(
    Guid UserId,
    Guid GameId,
    string Answer) : IOperationRequest<Exercise>;