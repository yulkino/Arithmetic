using Domain.Entity;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class StatisticCollector : IStatisticCollector
{
    private readonly IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>>
        _exerciseProgressStatisticsCalculator;

    private readonly IStatisticCalculator<List<GameStatistic>> _gameStatisticCalculator;

    private readonly IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeSpan>>
        _operationStatisticCalculator;

    public StatisticCollector(IStatisticCalculator<List<GameStatistic>> gameStatisticCalculator,
        IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeSpan>> operationStatisticCalculator,
        IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>>
            exerciseProgressStatisticsCalculator)
    {
        _gameStatisticCalculator = gameStatisticCalculator;
        _operationStatisticCalculator = operationStatisticCalculator;
        _exerciseProgressStatisticsCalculator = exerciseProgressStatisticsCalculator;
    }

    public async Task<Statistic> CollectStatistics(User user, List<ResolvedGame> resolvedGames, CancellationToken cancellationToken)
    {
        var statistic = new Statistic(user, resolvedGames);
        statistic.ExerciseProgressStatistic = await _exerciseProgressStatisticsCalculator.Calculate(resolvedGames, cancellationToken);
        statistic.OperationsStatistic = await _operationStatisticCalculator.Calculate(resolvedGames, cancellationToken);
        statistic.GameStatistic = await _gameStatisticCalculator.Calculate(resolvedGames, cancellationToken);
        return statistic;
    }

    public async Task<Statistic> UpdateStatistics(User user, List<ResolvedGame> resolvedGames, Statistic userStatistic, CancellationToken cancellationToken)
    {
        var newResolvedGames = resolvedGames.Except(userStatistic.ResolvedGame).ToList();

        if (!newResolvedGames.Any())
        {
            return userStatistic;
        }

        userStatistic.GameStatistic = await _gameStatisticCalculator.UpdateCalculations(
            newResolvedGames, userStatistic.GameStatistic!, cancellationToken);
        userStatistic.OperationsStatistic = await _operationStatisticCalculator.UpdateCalculations(
            newResolvedGames, userStatistic.OperationsStatistic!, cancellationToken);
        //TODO fix that OperationsStatistic add new and not edit exists
        userStatistic.ExerciseProgressStatistic = await _exerciseProgressStatisticsCalculator.UpdateCalculations(
            newResolvedGames, userStatistic.ExerciseProgressStatistic!, cancellationToken);

        return userStatistic;
    }
}