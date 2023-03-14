namespace API.DTOs.StatisticDtos;

public sealed record ExerciseProgressStatisticDto(
    DateTime ExercisesResolveDate,
    TimeOnly ExercisesResolveAverageDuration);