using Domain.Entity.ExerciseEntities;

namespace Application.Services.StatisticServices;

public static class CalculatorHelper
{
    public static TimeSpan CalculateAverageTimeSpanFromResolvedExercises(this List<ResolvedExercise> resolvedExercises)
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
}