using Domain.Entity.ExerciseEntities;

namespace Application.Mediators.GameMediator.SaveExercise;

public record SaveExerciseCommand(
    Guid UserId,
    Guid GameId,
    Guid ExerciseId,
    double Answer) : IOperationRequest<ResolvedExercise>;