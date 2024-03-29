﻿namespace Domain.Entity.SettingsEntities;

public class Settings : IEntity
{
    public Settings(Difficulty difficulty, HashSet<Operation> operations, int exerciseCount)
    {
        Id = Guid.NewGuid();
        Difficulty = difficulty;
        Operations = operations;
        ExerciseCount = exerciseCount;
    }

    private Settings() { }
    public Difficulty Difficulty { get; set; }
    public HashSet<Operation> Operations { get; set; }
    public int ExerciseCount { get; set; }
    public Guid Id { get; }
}