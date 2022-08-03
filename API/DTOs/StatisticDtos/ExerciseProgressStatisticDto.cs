namespace API.DTOs.StatisticDtos;

public sealed record ExerciseProgressStatisticDto(
    DateTime ExercisesResolveDate,
    double ExercisesResolveAverageDuration);