using API.DTOs.GameDtos;

namespace API.DTOs.StatisticDtos;

public sealed record GameStatisticDto(
    Guid Id,
    DateTime GameDate,
    int ExerciseCount,
    TimeOnly GameDuration,
    double CorrectAnswersPercentage) : GameDto(Id);