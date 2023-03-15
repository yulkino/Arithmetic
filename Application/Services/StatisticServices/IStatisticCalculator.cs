using Domain.Entity.GameEntities;

namespace Application.Services.StatisticServices;

public interface IStatisticCalculator<out TResult>
    where TResult : class
{
    TResult Calculate(List<ResolvedGame> resolvedGames);
}