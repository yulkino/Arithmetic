namespace API.DTOs.SettingsDtos;

//TODO int to Guid
//TODO feature get operations and Difficulties list
public sealed record SettingsDto(
    List<int> Operations,
    int Difficulty,
    int ExerciseCount);