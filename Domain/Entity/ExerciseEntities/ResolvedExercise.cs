﻿namespace Domain.Entity.ExerciseEntities;

public class ResolvedExercise : IEntity
{
    public ResolvedExercise(double userAnswer, TimeSpan elapsedTime, Exercise exercise)
    {
        Id = Guid.NewGuid();
        UserAnswer = userAnswer;
        ElapsedTime = elapsedTime;
        Exercise = exercise;

        IsCorrect = Math.Abs(Exercise.Answer - userAnswer) < 0.0001;
    }

    private ResolvedExercise() { }
    public double UserAnswer { get; }
    public TimeSpan ElapsedTime { get; }
    public bool IsCorrect { get; }
    public Exercise Exercise { get; }
    public Guid Id { get; }
}