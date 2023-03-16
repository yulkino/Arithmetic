using Domain.Entity.ExerciseEntities;
using Domain.Entity.SettingsEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public static class CalculatorHelper
{
    public static TimeSpan CalculateAverageTimeSpan(this List<ResolvedExercise> resolvedExercises)
    {
        var totalTimeElapsed = TimeSpan.Zero;
        resolvedExercises.ForEach(e =>
        {
            totalTimeElapsed += e.ElapsedTime;
        });

        var averageTimeElapsed = TimeSpan.Zero;
        if (totalTimeElapsed != TimeSpan.Zero && resolvedExercises.Count != 0)
            averageTimeElapsed = totalTimeElapsed / resolvedExercises.Count;

        return averageTimeElapsed;
    }

    public static TimeSpan RecalculateAverageTimeSpanWith(this OperationsStatistic operationsStatistics, 
        OperationsStatistic newOperationsStatistics)
    {
        TimeSpan averageTimeElapsed;

        var totalOperationsStatisticsTimeElapsed = operationsStatistics.Y * operationsStatistics.ResolvedExercisesCount;
        var totalNewOperationsStatisticsTimeElapsed = newOperationsStatistics.Y * newOperationsStatistics.ResolvedExercisesCount;

        var totalTimeElapsed = totalOperationsStatisticsTimeElapsed + totalNewOperationsStatisticsTimeElapsed;
        var totalResolvedExercisesCount = operationsStatistics.ResolvedExercisesCount + newOperationsStatistics.ResolvedExercisesCount;

        if (newOperationsStatistics.ResolvedExercisesCount > 0 && newOperationsStatistics.Y > TimeSpan.Zero)
            averageTimeElapsed = totalTimeElapsed / totalResolvedExercisesCount;
        else
            averageTimeElapsed = operationsStatistics.Y;

        return averageTimeElapsed;
    }
}