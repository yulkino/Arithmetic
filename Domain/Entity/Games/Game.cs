using Domain.Entity.Exercises;
using Shared;

namespace Domain.Entity.Games;

public class Game : IEntity
{
    public Guid Id { get; init; }
    public User User { get; init; }
    public Settings Settings { get; init; }
    public List<Exercise> Exercises { get; init; }
    public DateTime Date { get; init; }

    public Game(User user, Settings settings)
    {
        Id = Guid.NewGuid();
        User = user;
        Settings = settings;
        Exercises = new();
        Date = DateTime.Now;
    }

    public Exercise GiveNextExercise()
    {
        var operation = Settings.Operations.PickRandom();
        var max = (int) Math.Pow(10, Settings.Difficulty.MaxDigitCount);
        var first = Pick(max);
        var second = Pick(max);

        return new(first, second, operation);
        
        int Pick(int value) 
            => Random.Shared.Next(-value + 1, value);
    }
}
