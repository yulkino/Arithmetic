using Domain.Entity.GameEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class GameStatisticCalculator : IStatisticCalculator<List<GameStatistic>>
{
    public List<GameStatistic> Calculate(List<ResolvedGame> resolvedGames)
    {
        var gameStatistics = new List<GameStatistic>();
        resolvedGames.ForEach(resolvedGame =>
        {
            gameStatistics.Add(
                new GameStatistic
                {
                    GameDate = resolvedGame.Game.Date,
                    ExerciseCount = resolvedGame.ResolvedExercises.Count,
                    CorrectAnswersPercentage =
                        Math.Round(
                            (double)resolvedGame.CorrectAnswerCount * 100 /
                            resolvedGame.ResolvedExercises.Count,
                            2),
                    GameDuration = TimeOnly.FromTimeSpan(resolvedGame.ElapsedTime)
                });
        });
        return gameStatistics;
    }

    public List<GameStatistic> UpdateCalculations(List<ResolvedGame> newResolvedGames, List<GameStatistic> gameStatistic)
    {
        var newGameStatistic = Calculate(newResolvedGames);
        gameStatistic.AddRange(newGameStatistic);
        return gameStatistic;
    }
}