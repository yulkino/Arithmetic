using Domain.Entity;
using Domain.Entity.ExerciseEntities;
using Domain.Entity.GameEntities;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class OperationsStatisticCalculator : IStatisticCalculator<Diagram<OperationsStatistic, Operation, TimeSpan>>
{
    public Diagram<OperationsStatistic, Operation, TimeSpan> Calculate(List<ResolvedGame> resolvedGames)
    {
        var operationsStatistic = new Diagram<OperationsStatistic, Operation, TimeSpan>();
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

        return new OperationsStatistic(operation, resolvedOperationExercises.CalculateAverageTimeSpan(), resolvedOperationExercises.Count);
    }

    public Diagram<OperationsStatistic, Operation, TimeSpan> UpdateCalculations(List<ResolvedGame> newResolvedGames,
        Diagram<OperationsStatistic, Operation, TimeSpan> operationsStatistic)
    {
        var updatedStatistic = new Diagram<OperationsStatistic, Operation, TimeSpan>();

        var newOperationsStatistic = Calculate(newResolvedGames);

        foreach (var operationStatistic in operationsStatistic)
        {
            var newOperationStatistic = newOperationsStatistic.First(s => s.X == operationStatistic.X);

            var newAverageTimeSpan = operationStatistic.RecalculateAverageTimeSpanWith(newOperationStatistic);
            updatedStatistic.AddNode(new OperationsStatistic(
                operationStatistic.X, 
                newAverageTimeSpan, 
                operationStatistic.ResolvedExercisesCount + newOperationStatistic.ResolvedExercisesCount));
        }
        return updatedStatistic;
    }
}