namespace API.DTOs.SettingsDtos;

public sealed record SettingsDto(
    List<OperationDto> Operations,
    DifficultyDto Difficulty,
    int ExerciseCount);