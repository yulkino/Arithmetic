using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;

namespace Domain.Entity;

public class Statistic : IEntity
{
    public Statistic(User user, List<ResolvedGame> resolvedResolvedGame)
    {
        Id = Guid.NewGuid();
        User = user;
        ResolvedGame = resolvedResolvedGame;
    }

    private Statistic() { }
    public User User { get; }
    public List<GameStatistic>? GameStatistic { get; set; }
    public Diagram<OperationsStatistic, Operation, TimeSpan>? OperationsStatistic { get; set; }
    public Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>? ExerciseProgressStatistic { get; set; }
    public List<ResolvedGame> ResolvedGame { get; }
    public Guid Id { get; }
}