namespace API.DTOs.StatisticDtos;

public sealed class GameStatisticDto
{
    public DateTime GameDate { get; set; }
    public int ExerciseCount { get; set; }
    public TimeSpan GameDuration { get; set; }
    public double CorrectAnswersPercentage { get; set; }
}