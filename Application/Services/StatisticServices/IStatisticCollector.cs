using Domain.Entity;
using Domain.Entity.GameEntities;

namespace Application.Services.StatisticServices;

public interface IStatisticCollector
{
    Task<Statistic> CollectStatistics(User user, List<ResolvedGame> resolvedGames, CancellationToken cancellationToken);
    Task<Statistic> UpdateStatistics(User user, List<ResolvedGame> resolvedGames, Statistic userStatistic, CancellationToken cancellationToken);
}