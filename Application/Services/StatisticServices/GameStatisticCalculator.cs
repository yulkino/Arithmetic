using Domain.Entity.GameEntities;
using Domain.StatisticStaff;

namespace Application.Services.StatisticServices;

public class GameStatisticCalculator : IStatisticCalculator<List<GameStatistic>>
{
    public Task<List<GameStatistic>> Calculate(List<ResolvedGame> resolvedGames, CancellationToken cancellationToken)
    {
        var gameStatistics = new List<GameStatistic>();
        foreach (var resolvedGame in resolvedGames)
        {
            var correctAnswersPercentage = Math.Round((double)resolvedGame.CorrectAnswerCount * 100 /
                                                      resolvedGame.ResolvedExercises.Count, 2);
            gameStatistics.Add(
                new GameStatistic
                {
                    GameDate = resolvedGame.Game.Date,
                    ExerciseCount = resolvedGame.ResolvedExercises.Count,
                    CorrectAnswersPercentage = double.IsNaN(correctAnswersPercentage) ? 0 : 
                        correctAnswersPercentage,
                    GameDuration = resolvedGame.ElapsedTime
                });
        }

        return Task.FromResult(gameStatistics);
    }

    public async Task<List<GameStatistic>> UpdateCalculations(List<ResolvedGame> newResolvedGames,
        List<GameStatistic> gameStatistic, CancellationToken cancellationToken)
    {
        var newGameStatistic = await Calculate(newResolvedGames, cancellationToken);
        gameStatistic.AddRange(newGameStatistic);
        return gameStatistic;
    }
}