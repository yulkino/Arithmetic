using Domain.Entity.SettingsEntities;

namespace Application.Mediators.SettingsMediator.Edit;

public record EditSettingsCommand(
    Guid UserId,
    List<OperationIdItemDto> Operations,
    DifficultyIdItemDto Difficulty,
    int ExerciseCount) : IOperationRequest<Settings>;

public sealed record OperationIdItemDto(Guid Id);
public sealed record DifficultyIdItemDto(Guid Id);