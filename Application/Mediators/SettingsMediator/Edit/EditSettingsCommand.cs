using Domain.Entity.SettingsEntities;

namespace Application.Mediators.SettingsMediator.Edit;

public record EditSettingsCommand(
    Guid UserId,
    List<OperationItemDto> Operations,
    DifficultyItemDto Difficulty,
    int ExerciseCount) : IOperationRequest<Settings>;

public sealed record OperationItemDto(Guid Id, string Name);
public sealed record DifficultyItemDto(Guid Id, string Name);