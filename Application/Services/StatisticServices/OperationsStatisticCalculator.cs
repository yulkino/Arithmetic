using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class OperationsStatisticCalculator : IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeOnly>>
{
    public Diagram<OperationsStatistic, Operation, TimeOnly> Calculate(List<ResolvedGame> resolvedGames)
    {
        var operationsStatistic = new Diagram<OperationsStatistic, Operation, TimeOnly>();
        var resolvedExercises = resolvedGames
            .SelectMany(g => g.ResolvedExercises)
            .ToList();

        operationsStatistic.AddNode(CalculateOperationStatistic(resolvedExercises, Operation.Addition));
        operationsStatistic.AddNode(CalculateOperationStatistic(resolvedExercises, Operation.Subtraction));
        operationsStatistic.AddNode(CalculateOperationStatistic(resolvedExercises, Operation.Multiplication));
        operationsStatistic.AddNode(CalculateOperationStatistic(resolvedExercises, Operation.Division));
        return operationsStatistic;
    }

    private OperationsStatistic CalculateOperationStatistic(List<ResolvedExercise> resolvedExercises,
        Operation operation)
    {
        var resolvedOperationExercises = resolvedExercises
            .Where(e => e.Exercise.Operation == operation)
            .ToList();

        return new OperationsStatistic(operation,
            TimeOnly.FromTimeSpan(resolvedOperationExercises.CalculateAverageTimeSpanFromResolvedExercises()));
    }
}