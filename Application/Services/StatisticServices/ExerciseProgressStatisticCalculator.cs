using Domain.Entity.GameEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class ExerciseProgressStatisticCalculator : IStatisticCalculator<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>>
{
    public Task<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>> Calculate(List<ResolvedGame> resolvedGames,
        CancellationToken cancellationToken)
    {
        var exerciseProgressStatistic = new Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>();
        if(!resolvedGames.Any())
            return Task.FromResult(exerciseProgressStatistic);

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

        return Task.FromResult(exerciseProgressStatistic);
    }

    public async Task<Diagram<ExerciseProgressStatistic, DateTime, TimeSpan>> UpdateCalculations(
        List<ResolvedGame> newResolvedGames,
        Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> exerciseProgressStatistic,
        CancellationToken cancellationToken)
    {
        if(!newResolvedGames.Any())
            return exerciseProgressStatistic;

        var newResolvedGameDates = newResolvedGames
            .Select(g => g.Game.Date.Date)
            .Distinct()
            .OrderBy(x => x.Date)
            .ToList();

        var newExerciseProgressStatistic = await Calculate(newResolvedGames, cancellationToken);

        var earliestResolvedGameDate = newResolvedGameDates.MaxBy(x => x.Date);
        var containsIntersectingDate = exerciseProgressStatistic
            .Select(e => e.X.Date)
            .ToList()
            .Contains(earliestResolvedGameDate);

        if (!containsIntersectingDate)
        {
            return exerciseProgressStatistic
                .Concat(newExerciseProgressStatistic)
                .ToDiagram<ExerciseProgressStatistic, DateTime, TimeSpan>();
        }

        return UpdateStatisticIncludingIntersectingDate(
            exerciseProgressStatistic,
            newExerciseProgressStatistic,
            earliestResolvedGameDate);
    }

    private Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> UpdateStatisticIncludingIntersectingDate(
        Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> exerciseProgressStatistic,
        Diagram<ExerciseProgressStatistic, DateTime, TimeSpan> newExerciseProgressStatistic,
        DateTime intersectingDate)
    {
        var oldStatisticOfIntersectingDate =
            exerciseProgressStatistic.Single(e => e.X.Date == intersectingDate);
        var newStatisticOfIntersectingDate =
            newExerciseProgressStatistic.Single(e => e.X.Date == intersectingDate);

        var newAverageTimeSpan = oldStatisticOfIntersectingDate
            .RecalculateAverageTimeSpanWith<ExerciseProgressStatistic, DateTime, TimeSpan>(
                newStatisticOfIntersectingDate);
        var newElementCount = oldStatisticOfIntersectingDate.ElementCountStatistic +
                              newStatisticOfIntersectingDate.ElementCountStatistic;

        var oldStatisticList = exerciseProgressStatistic.ToList();
        oldStatisticOfIntersectingDate.UpdateAverageDuration(newAverageTimeSpan, newElementCount);

        newExerciseProgressStatistic.Remove(newStatisticOfIntersectingDate);
        oldStatisticList.AddRange(newExerciseProgressStatistic);

        return oldStatisticList.ToDiagram<ExerciseProgressStatistic, DateTime, TimeSpan>();
    }
}