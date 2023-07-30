using Domain.Entity.GameEntities;

namespace Application.Services.StatisticServices;

public interface IStatisticCalculator<TResult>
    where TResult : class
{
    Task<TResult> Calculate(List<ResolvedGame> resolvedGames, CancellationToken cancellationToken = default);
    Task<TResult> UpdateCalculations(List<ResolvedGame> newResolvedGames, TResult statistic, CancellationToken cancellationToken = default);
}