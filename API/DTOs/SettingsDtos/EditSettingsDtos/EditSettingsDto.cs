namespace API.DTOs.SettingsDtos.EditSettingsDtos;

public record EditSettingsDto(
    List<Guid> OperationIds,
    Guid DifficultyId,
    int ExerciseCount);