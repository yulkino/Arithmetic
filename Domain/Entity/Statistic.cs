using Domain.Entity.GameEntities;
using Domain.StatisticStaff;
using Domain.Entity.SettingsEntities;

namespace Domain.Entity;

public class Statistic : IEntity
{
    public Guid Id { get; }
    public User User { get; }
    public List<GameStatistic> GameStatisticList { get; }
    public Diagram<OperationsStatistic, Operation, double> OperationsStatisticList { get; }
    public Diagram<ExerciseProgressStatistic, DateTime, double> ExerciseProgressStatisticList { get; }
    public List<ResolvedGame> ResolvedGame { get; }

    public Statistic(User user, List<ResolvedGame> resolvedResolvedGame)
    {
        Id = Guid.NewGuid();
        User = user;
        ResolvedGame = resolvedResolvedGame;
        GameStatisticList = new List<GameStatistic>();
        OperationsStatisticList = new Diagram<OperationsStatistic, Operation, double>();
        ExerciseProgressStatisticList = new Diagram<ExerciseProgressStatistic, DateTime, double>();
    }
}
