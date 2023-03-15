using Domain.Entity;
using Domain.Entity.GameEntities;

namespace Application.Services.StatisticServices;

public interface IStatisticCollector
{
    Statistic CollectStatistics(User user, List<ResolvedGame> resolvedGames);
}