﻿using Domain.Entity.SettingsEntities;

namespace Domain.Entity.ExerciseEntities;

public class Exercise : IEntity
{
    public Exercise(double leftOperand, double rightOperand, Operation operation)
    {
        Id = Guid.NewGuid();
        LeftOperand = leftOperand;
        RightOperand = rightOperand;
        Operation = operation;

        StartTime = DateTime.Now;

        Answer = Operation.Act(leftOperand, rightOperand);
    }

    private Exercise() { }
    public double LeftOperand { get; }
    public double RightOperand { get; }
    public Operation Operation { get; }
    public double Answer { get; }
    public DateTime StartTime { get; }
    public Guid Id { get; }

    public ResolvedExercise Resolve(double userAnswer)
    {
        return new ResolvedExercise(userAnswer, DateTime.Now - StartTime, this);
    }
}