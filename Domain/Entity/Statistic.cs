namespace Domain.Entity;

public class Statistic : IEntity
{
    public Statistic()
    {
        Id = Guid.NewGuid();
        OperationsStatisticList = new List<OperationsStatistic>();
        ExerciseProgressStatisticList = new List<ExerciseProgressStatistic>();
        GameStatisticList = new List<GameStatistic>();
    }

    public Guid Id { get; }
    public List<GameStatistic> GameStatisticList { get; init; }
    public List<OperationsStatistic> OperationsStatisticList { get; init; }
    public List<ExerciseProgressStatistic> ExerciseProgressStatisticList { get; init; }

    public sealed record GameStatistic(
        DateTime GameDate,
        int ExerciseCount,
        double GameDuration,
        double CorrectAnswersPercentage);

    public sealed record OperationsStatistic(
        int Operation,
        double GameAverageDuration);

    public sealed record ExerciseProgressStatistic(
        DateTime ExercisesResolveDate,
        double ExercisesResolveAverageDuration);
}
