using Domain.Entity;

namespace Application.Mediators.SettingsMediator.Edit;

public record EditSettingsCommand(
    Guid UserId,
    List<int> Operations,
    int Difficulty,
    int ExerciseCount) : IOperationRequest<Settings>;