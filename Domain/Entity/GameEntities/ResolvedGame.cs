using Domain.Entity.ExerciseEntities;

namespace Domain.Entity.GameEntities;

public class ResolvedGame : IEntity, IEquatable<ResolvedGame>
{
    public Guid Id { get; init; }
    public Game Game { get; init; }
    public int CorrectAnswerCount { get; private set; }
    public TimeSpan ElapsedTime { get; private set; }
    public List<ResolvedExercise> ResolvedExercises { get; set; }

    public ResolvedGame(Game game)
    {
        Id = Guid.NewGuid();
        Game = game;
        ResolvedExercises = new List<ResolvedExercise>();
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

    public bool Equals(ResolvedGame? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ResolvedGame)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
