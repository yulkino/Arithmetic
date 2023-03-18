using Domain.Entity.ExerciseEntities;
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

    public static TimeSpan RecalculateAverageTimeSpanWith<TElement, TX, TY>(
        this TElement statisticsElement, TElement newStatisticsElement)
        where TElement : IStatisticElement<TX, TimeSpan>
    {
        var totalStatisticsTimeElapsed = statisticsElement.Y * statisticsElement.ElementCountStatistic;
        var totalNewStatisticsTimeElapsed = newStatisticsElement.Y * newStatisticsElement.ElementCountStatistic;

        var totalTimeElapsed = totalStatisticsTimeElapsed + totalNewStatisticsTimeElapsed;
        var totalResolvedExercisesCount = statisticsElement.ElementCountStatistic + newStatisticsElement.ElementCountStatistic;
        var averageTimeElapsed = totalTimeElapsed / totalResolvedExercisesCount;

        return averageTimeElapsed;
    }
}