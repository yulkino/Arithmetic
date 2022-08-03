namespace API.DTOs.StatisticDtos;

public sealed record StatisticDto(
    List<GameStatisticDto> GameStatistic,
    List<OperationsStatisticDto> OperationsStatistic,
    List<ExerciseProgressStatisticDto> ExerciseProgressStatistic);