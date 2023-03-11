using Domain.Entity.ExerciseEntities;

namespace Domain.Entity.GameEntities;

public class ResolvedGame : IEntity
{
    public Guid Id { get; init; }
    public Game Game { get; init; }
    public int CorrectAnswerCount { get; private set; }
    public TimeSpan ElapsedTime { get; private set; }
    public List<ResolvedExercise> ResolvedExercises { get; set; } = new List<ResolvedExercise>();

    public ResolvedGame(Game game)
    {
        Id = Guid.NewGuid();
        Game = game;
    }

    public ResolvedGame ProcessGameResult()
    {
        var elapsedTime = new TimeSpan();
        ResolvedExercises.ForEach(r =>
        {
            elapsedTime += r.ElapsedTime;
        });
        ElapsedTime = elapsedTime;

        CorrectAnswerCount = ResolvedExercises.Count(r => r.IsCorrect);

        return this;
    }
}
