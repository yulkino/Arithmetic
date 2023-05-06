using Domain.Entity.ExerciseEntities;
using Domain.Entity.SettingsEntities;
using Shared;

namespace Domain.Entity.GameEntities;

public class Game : IEntity, IEquatable<Game>
{
    public Game(User user, Settings settings)
    {
        Id = Guid.NewGuid();
        User = user;
        Settings = settings;
        Exercises = new List<Exercise>();
        Date = DateTime.Now;
    }

    private Game() { }
    public User User { get; }
    public Settings Settings { get; }
    public List<Exercise> Exercises { get; }
    public DateTime Date { get; }
    public Guid Id { get; }

    public bool Equals(Game? other)
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

    public Exercise GiveNextExercise()
    {
        var operation = Settings.Operations.PickRandom();
        var max = (int)Math.Pow(10, Settings.Difficulty.MaxDigitCount);
        var first = Pick(max);
        var second = Pick(max);

        return new Exercise(first, second, operation);

        int Pick(int value) => Random.Shared.Next(-value + 1, value);
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

        return Equals((Game)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}