namespace Domain.Entity.SettingsEntities;

public class Settings : IEntity
{
    public Guid Id { get; }
    public Difficulty Difficulty { get; set; }
    public HashSet<Operation> Operations { get; set; }
    public int ExerciseCount { get; set; }

    public Settings(Difficulty difficulty, HashSet<Operation> operations, int exerciseCount)
    {
        Id = Guid.NewGuid();
        Difficulty = difficulty;
        Operations = operations;
        ExerciseCount = exerciseCount;
    }

    private Settings(){ }
}
