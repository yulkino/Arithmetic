namespace API.DTOs.SettingsDtos.GetSettingsDtos;

public sealed record SettingsDto(
    List<OperationDto> Operations,
    DifficultyDto Difficulty,
    int ExerciseCount);