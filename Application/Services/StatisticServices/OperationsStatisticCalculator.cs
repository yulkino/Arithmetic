using Application.ServiceContracts.Repositories.Read.SettingsReadRepositories;
using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class OperationsStatisticCalculator : IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeSpan>>
{
    private readonly IOperationsReadRepository _operationsReadRepository;

    public OperationsStatisticCalculator(IOperationsReadRepository operationsReadRepository)
    {
        _operationsReadRepository = operationsReadRepository;
    }

    public async Task<Diagram<OperationsStatistic, Operation, TimeSpan>> Calculate(List<ResolvedGame> resolvedGames,
        CancellationToken cancellationToken)
    {
        var operationsStatistic = new Diagram<OperationsStatistic, Operation, TimeSpan>();
        if (!resolvedGames.Any())
            return operationsStatistic;

        var resolvedExercises = resolvedGames
            .SelectMany(g => g.ResolvedExercises)
            .ToList();

        var operations = await _operationsReadRepository.GetOperationsAsync(cancellationToken);
        foreach (var operation in operations)
        {
            operationsStatistic.AddNode(CalculateOperationStatistic(resolvedExercises, operation));
        }

        return operationsStatistic;
    }

    public async Task<Diagram<OperationsStatistic, Operation, TimeSpan>> UpdateCalculations(List<ResolvedGame> newResolvedGames,
        Diagram<OperationsStatistic, Operation, TimeSpan> operationsStatistic, CancellationToken cancellationToken)
    {
        if(!newResolvedGames.Any())
            return operationsStatistic;

        var newOperationsStatistic = await Calculate(newResolvedGames, cancellationToken);

        foreach (var operationStatistic in operationsStatistic)
        {
            var newOperationStatistic = newOperationsStatistic.Single(s => s.X == operationStatistic.X);
            if (newOperationStatistic.ElementCountStatistic == 0 || newOperationStatistic.Y == TimeSpan.Zero)
                continue;

            var newAverageTimeSpan = operationStatistic
                .RecalculateAverageTimeSpanWith<OperationsStatistic, Operation, TimeSpan>(newOperationStatistic);
            var newElementCount = operationStatistic.ElementCountStatistic +
                                  newOperationStatistic.ElementCountStatistic;
            operationStatistic.UpdateAverageDuration(newAverageTimeSpan, newElementCount);
        }
        return operationsStatistic;
    }

    private OperationsStatistic CalculateOperationStatistic(List<ResolvedExercise> resolvedExercises,
        Operation operation)
    {
        var resolvedOperationExercises = resolvedExercises
            .Where(e => e.Exercise.Operation == operation)
            .ToList();

        return new OperationsStatistic(operation, resolvedOperationExercises.CalculateAverageTimeSpan(),
            resolvedOperationExercises.Count);
    }
}