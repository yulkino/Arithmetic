namespace API.DTOs.SettingsDtos.EditSettingsDtos;

public record EditSettingsDto(
    List<OperationIdDto> Operations,
    DifficultyIdDto Difficulty,
    int ExerciseCount);