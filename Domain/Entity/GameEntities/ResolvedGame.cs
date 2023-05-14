using Domain.Entity.ExerciseEntities;

namespace Domain.Entity.GameEntities;

public class ResolvedGame : IEntity, IEquatable<ResolvedGame>
{
    public ResolvedGame(Game game)
    {
        Id = Guid.NewGuid();
        Game = game;
    }

    private ResolvedGame() { }
    public Game Game { get; }
    public int CorrectAnswerCount { get; private set; }
    public TimeSpan ElapsedTime { get; private set; }
    public List<ResolvedExercise> ResolvedExercises { get; } = new();
    public Guid Id { get; }

    public bool Equals(ResolvedGame? other)
    {
        if (other is null)
            return false;

        return ReferenceEquals(this, other) || Id.Equals(other.Id);
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

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((ResolvedGame)obj);
    }

    public override int GetHashCode() => Id.GetHashCode();
}