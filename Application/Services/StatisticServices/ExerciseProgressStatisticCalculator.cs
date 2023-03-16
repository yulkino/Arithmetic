using Domain.Entity.GameEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class ExerciseProgressStatisticCalculator : IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>>
{
    public Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> Calculate(List<ResolvedGame> resolvedGames)
    {
        var exerciseProgressStatistic = new Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>();
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

            var averageTimeElapsed = resolvedExercises.CalculateAverageTimeSpan();

            exerciseProgressStatistic.AddNode(new ExerciseProgressStatistic(
                dateTime, 
                averageTimeElapsed, 
                resolvedExercises.Count));
        }
        return exerciseProgressStatistic;
    }

    public Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> UpdateCalculations(List<ResolvedGame> newResolvedGames, 
        Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> exerciseProgressStatistic)
    {
        throw new NotImplementedException();
    }
}