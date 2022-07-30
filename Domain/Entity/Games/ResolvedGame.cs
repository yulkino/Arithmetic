using Domain.Entity.Exercises;

namespace Domain.Entity.Games;

public class ResolvedGame
{
    public Guid Id { get; init; }

    public Game Game { get; init; }

    public int CorrectAnswerCount { get; }

    public TimeSpan ElapsedTime { get; init; }

    public List<ResolvedExercise> ResolvedExercises { get; init; }

    public ResolvedGame(Game game, TimeSpan elapsedTime, List<ResolvedExercise> resolvedExercises)
    {
        Id = Guid.NewGuid();
        Game = game;
        ElapsedTime = elapsedTime;
        ResolvedExercises = resolvedExercises;

        CorrectAnswerCount = ResolvedExercises.Count(r => r.IsCorrect);
    }
}
