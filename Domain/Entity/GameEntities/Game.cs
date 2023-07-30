using Domain.Entity.ExerciseEntities;
using Domain.Entity.SettingsEntities;
using Shared;

namespace Domain.Entity.GameEntities;

public class Game : IEntity, IEquatable<Game>
{
    public Game(User user, Settings settings, DateTime creationDate)
    {
        Id = Guid.NewGuid();
        User = user;
        Settings = settings;
        Exercises = new List<Exercise>();
        Date = creationDate;
    }

    private Game() { }
    public User User { get; }
    public Settings Settings { get; }
    public List<Exercise> Exercises { get; }
    public DateTime Date { get; }
    public Guid Id { get; }

    public bool Equals(Game? other)
    {
        if (other is null)
            return false;

        return ReferenceEquals(this, other) || Id.Equals(other.Id);
    }

    public Exercise GiveNextExercise(DateTime startTime)
    {
        var operation = Settings.Operations.PickRandom();
        var max = (int)Math.Pow(10, Settings.Difficulty.MaxDigitCount);
        var first = Pick(max);
        var second = Pick(max);

        var exercise = new Exercise(first, second, operation, startTime);
        Exercises.Add(exercise);
        return exercise;

        int Pick(int value) => Random.Shared.Next(-value + 1, value);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        return obj.GetType() == GetType() && Equals((Game)obj);
    }

    public override int GetHashCode() => Id.GetHashCode();
}