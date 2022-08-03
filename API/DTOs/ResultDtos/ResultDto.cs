namespace API.DTOs.ResultDtos;

public sealed record ResultDto(
    int ExerciseCount,
    double GameDuration,
    List<ExerciseResultDto> Exercises);