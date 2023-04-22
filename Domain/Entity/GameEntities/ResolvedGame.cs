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
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Id.Equals(other.Id);
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
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        return Equals((ResolvedGame)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}