using Domain.StatisticStaff;

namespace Domain.Entity;

public class Statistic : IEntity
{
    public Statistic()
    {
        Id = Guid.NewGuid();
        GameStatisticList = new List<GameStatistic>();
        OperationsStatisticList = new Diagram<OperationsStatistic, Operation, double>();
        ExerciseProgressStatisticList = new Diagram<ExerciseProgressStatistic, DateTime, double>();
    }

    public Guid Id { get; }
    public List<GameStatistic> GameStatisticList { get; }
    public Diagram<OperationsStatistic, Operation, double> OperationsStatisticList { get; }
    public Diagram<ExerciseProgressStatistic, DateTime, double> ExerciseProgressStatisticList { get; }
}
