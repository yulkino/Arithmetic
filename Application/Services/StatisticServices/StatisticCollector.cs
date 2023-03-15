using Domain.Entity;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class StatisticCollector : IStatisticCollector
{
    private readonly IStatisticCalculator<List<GameStatistic>> _gameStatisticCalculator;
    private readonly IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeOnly>> _operationStatisticCalculator;
    private readonly IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeOnly>> _exerciseProgressStatisticsCalculator;

    public StatisticCollector(IStatisticCalculator<List<GameStatistic>> gameStatisticCalculator,
        IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeOnly>> operationStatisticCalculator,
        IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeOnly>> exerciseProgressStatisticsCalculator)
    {
        _gameStatisticCalculator = gameStatisticCalculator;
        _operationStatisticCalculator = operationStatisticCalculator;
        _exerciseProgressStatisticsCalculator = exerciseProgressStatisticsCalculator;
    }

    public Statistic CollectStatistics(User user, List<ResolvedGame> resolvedGames)
        => new(user, resolvedGames)
        {
            GameStatisticList = _gameStatisticCalculator.Calculate(resolvedGames),
            OperationsStatisticList = _operationStatisticCalculator.Calculate(resolvedGames),
            ExerciseProgressStatisticList = _exerciseProgressStatisticsCalculator.Calculate(resolvedGames)
        };
}