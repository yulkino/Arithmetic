using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;

namespace Domain.Entity;

public class Statistic : IEntity
{
    public Guid Id { get; }
    public User User { get; }
    public List<GameStatistic>? GameStatisticList { get; set; }
    public Diagram<OperationsStatistic, Operation, TimeSpan>? OperationsStatisticList { get; set; }
    public Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>? ExerciseProgressStatisticList { get; set; }
    public List<ResolvedGame> ResolvedGame { get; }

    public Statistic(User user, List<ResolvedGame> resolvedResolvedGame)
    {
        Id = Guid.NewGuid();
        User = user;
        ResolvedGame = resolvedResolvedGame;
    }
}
