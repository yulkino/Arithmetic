using Domain.Entity.GameEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class ExerciseProgressStatisticCalculator : IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>>
{
    public Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> Calculate(List<ResolvedGame> resolvedGames)
    {
        var exerciseProgressStatistic = new Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>();
        var resolvedGameDates = resolvedGames
            .Select(g => g.Game.Date.Date)
            .Distinct()
            .ToList();

        foreach (var dateTime in resolvedGameDates)
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
        var newResolvedGameDates = newResolvedGames
            .Select(g => g.Game.Date.Date)
            .Distinct()
            .ToList();

        var newExerciseProgressStatistic = Calculate(newResolvedGames);

        var earliestResolvedGameDate = newResolvedGameDates.First();
        var containsIntersectingDate = exerciseProgressStatistic
            .Select(e => e.X.Date)
            .ToList()
            .Contains(earliestResolvedGameDate);
        if (containsIntersectingDate)
        {
            var oldStatisticOfIntersectingDate =
                exerciseProgressStatistic.First(e => e.X.Date == earliestResolvedGameDate);
            var newStatisticOfIntersectingDate =
                newExerciseProgressStatistic.First(e => e.X.Date == earliestResolvedGameDate);

            var newAverageTimeSpan = oldStatisticOfIntersectingDate
                .RecalculateAverageTimeSpanWith<ExerciseProgressStatistic, DateTime, TimeSpan>(
                    newStatisticOfIntersectingDate);
            var newStatisticElement = new ExerciseProgressStatistic(
                oldStatisticOfIntersectingDate.X,
                newAverageTimeSpan,
                oldStatisticOfIntersectingDate.ElementCountStatistic +
                newStatisticOfIntersectingDate.ElementCountStatistic);

            var oldStatisticList = exerciseProgressStatistic.ToList();

            oldStatisticList.Remove(oldStatisticOfIntersectingDate);

            var newStatistic = newExerciseProgressStatistic.ToList();
            newStatistic.Remove(newStatisticOfIntersectingDate);

            oldStatisticList.Add(newStatisticElement);
            oldStatisticList.AddRange(newStatistic);

            return oldStatisticList.ToDiagram<ExerciseProgressStatistic, DateTime, TimeSpan>();
        }
        return exerciseProgressStatistic
            .Concat(newExerciseProgressStatistic)
            .ToDiagram<ExerciseProgressStatistic, DateTime, TimeSpan>();
    }
}