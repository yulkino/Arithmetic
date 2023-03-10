using System.Diagnostics;
using Domain.Entity.SettingsEntities;

namespace Domain.Entity.ExerciseEntities;

public  class Exercise : IEntity
{
    public Guid Id { get; init; }
    public double LeftOperand { get; init; }
    public double RightOperand { get; init; }
    public Operation Operation { get; init; }
    public double Answer { get; }
    private DateTime _startTime; 

    public Exercise(double leftOperand, double rightOperand, Operation operation)
    {
        Id = Guid.NewGuid();
        LeftOperand = leftOperand;
        RightOperand = rightOperand;
        Operation = operation;

       _startTime = DateTime.Now;

        Answer = Operation.Act(leftOperand, rightOperand);
    }

    public ResolvedExercise Resolve(double userAnswer)
        => new(userAnswer, DateTime.Now - _startTime, this);
}
