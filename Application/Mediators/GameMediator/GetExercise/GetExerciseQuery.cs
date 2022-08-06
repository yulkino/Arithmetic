using Domain.Entity.Exercises;

namespace Application.Mediators.GameMediator.GetExercise;

public record GetExerciseQuery(
    Guid UserId,
    Guid GameId) : IOperationRequest<Exercise>;