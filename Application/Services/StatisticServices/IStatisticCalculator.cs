using Domain.Entity.GameEntities;

namespace Application.Services.StatisticServices;

public interface IStatisticCalculator<TResult>
    where TResult : class
{
    TResult Calculate(List<ResolvedGame> resolvedGames);
    TResult UpdateCalculations(List<ResolvedGame> newResolvedGames, TResult statistic);
}