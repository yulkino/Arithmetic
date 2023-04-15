using Domain.Entity;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class StatisticCollector : IStatisticCollector
{
    private readonly IStatisticCalculator<List<GameStatistic>> _gameStatisticCalculator;
    private readonly IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeSpan>> _operationStatisticCalculator;
    private readonly IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>> _exerciseProgressStatisticsCalculator;

    public StatisticCollector(IStatisticCalculator<List<GameStatistic>> gameStatisticCalculator,
        IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeSpan>> operationStatisticCalculator,
        IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>> exerciseProgressStatisticsCalculator)
    {
        _gameStatisticCalculator = gameStatisticCalculator;
        _operationStatisticCalculator = operationStatisticCalculator;
        _exerciseProgressStatisticsCalculator = exerciseProgressStatisticsCalculator;
    }

    public Statistic CollectStatistics(User user, List<ResolvedGame> resolvedGames)
    {
        return new(user, resolvedGames)
        {
            GameStatisticList = _gameStatisticCalculator.Calculate(resolvedGames),
            OperationsStatisticList = _operationStatisticCalculator.Calculate(resolvedGames),
            ExerciseProgressStatisticList = _exerciseProgressStatisticsCalculator.Calculate(resolvedGames)
        };
    }

    public Statistic UpdateStatistics(User user, List<ResolvedGame> resolvedGames, Statistic userStatistic)
    {
        var newResolvedGames = resolvedGames.Except(userStatistic.ResolvedGame).ToList();

        if (!newResolvedGames.Any())
            return userStatistic;

        userStatistic.GameStatisticList = _gameStatisticCalculator.UpdateCalculations(
            newResolvedGames, userStatistic.GameStatisticList!);
        userStatistic.OperationsStatisticList = _operationStatisticCalculator.UpdateCalculations(
            newResolvedGames, userStatistic.OperationsStatisticList!);
        userStatistic.ExerciseProgressStatisticList = _exerciseProgressStatisticsCalculator.UpdateCalculations(
            newResolvedGames, userStatistic.ExerciseProgressStatisticList!);

        return userStatistic;
    }
}