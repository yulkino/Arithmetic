using Domain.Entity.SettingsEntities;

namespace Application.Mediators.SettingsMediator.Edit;

public record EditSettingsCommand(
    Guid UserId,
    List<Guid> OperationIds,
    Guid DifficultyId,
    int ExerciseCount) : IOperationRequest<Settings>;