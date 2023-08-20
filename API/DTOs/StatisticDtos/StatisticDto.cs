namespace API.DTOs.StatisticDtos;

public sealed class StatisticDto
{
    public List<GameStatisticDto>? GameStatistic { get; set; }
    public List<OperationsStatisticDto>? OperationsStatistic { get; set; }
    public List<ExerciseProgressStatisticDto>? ExerciseProgressStatistic { get; set; }
}