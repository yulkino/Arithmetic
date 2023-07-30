namespace API.DTOs.StatisticDtos;

public sealed class ExerciseProgressStatisticDto
{
    public DateTime ExercisesResolveDate { get; set; }
    public TimeOnly ExercisesResolveAverageDuration { get; set; }
}