namespace API.DTOs.SettingsDtos;

public sealed record SettingsDto(
    List<int> Operations,
    int Difficulty,
    int DurationInMinutes);