using API.DTOs.GameDtos;

namespace API.DTOs.StatisticDtos;

public sealed record GameStatisticDto(
    Guid Id,
    DateTime GameDate,
    int ExerciseCount,
    double GameDuration,
    double CorrectAnswersPercentage) : GameDto(Id);