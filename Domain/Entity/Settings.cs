namespace Domain.Entity;

public class Settings
{
    public Guid Id { get; init; }
    public Difficulty Difficulty { get; init; }
    public HashSet<Operation> Operations { get; init; }
    public int ExerciseCount { get; init; }

    public Settings(Difficulty difficulty, HashSet<Operation> operations, int exerciseCount)
    {
        Id = Guid.NewGuid();
        Difficulty = difficulty;
        Operations = operations;
        ExerciseCount = exerciseCount;
    }
}
