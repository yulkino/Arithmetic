namespace Domain.StatisticStaff;

public sealed class GameStatistic
{
    public GameStatistic() => Id = Guid.NewGuid();

    public GameStatistic(double correctAnswersPercentage, int exerciseCount, DateTime gameDate, TimeSpan gameDuration) : this()
    {
        CorrectAnswersPercentage = correctAnswersPercentage;
        ExerciseCount = exerciseCount;
        GameDate = gameDate;
        GameDuration = gameDuration;
    }

    public Guid Id { get; init; }
    public DateTime GameDate { get; set; }
    public int ExerciseCount { get; set; }
    public TimeSpan GameDuration { get; set; }
    public double CorrectAnswersPercentage { get; set; }
}