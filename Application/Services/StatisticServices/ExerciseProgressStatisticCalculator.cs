using Domain.Entity.GameEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class ExerciseProgressStatisticCalculator : IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeOnly>>
{
    public Diagram<ExerciseProgressStatistic, DateTime, TimeOnly> Calculate(List<ResolvedGame> resolvedGames)
    {
        var exerciseProgressStatistic = new Diagram<ExerciseProgressStatistic, DateTime, TimeOnly>();
        var resolvedGameDate = resolvedGames
            .Select(g => g.Game.Date.Date)
            .Distinct()
            .ToList();

        foreach (var dateTime in resolvedGameDate)
        {
            var resolvedExercises = resolvedGames
                .Where(g => g.Game.Date.Date == dateTime)
                .SelectMany(e => e.ResolvedExercises)
                .ToList();

            var averageTimeElapsed = resolvedExercises.CalculateAverageTimeSpanFromResolvedExercises();

            exerciseProgressStatistic.AddNode(new ExerciseProgressStatistic(dateTime, TimeOnly.FromTimeSpan(averageTimeElapsed)));
        }
        return exerciseProgressStatistic;
    }
}