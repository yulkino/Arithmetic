using Domain.Entity.Operations;

namespace Domain.Entity.Exercises;

public  class Exercise
{
    public Guid Id { get; init; }

    public double LeftOperand { get; init; }
    
    public double RightOperand { get; init; }

    public Operation Operation { get; init; }

    public double Answer { get; }

    public Exercise(double leftOperand, double rightOperand, Operation operation)
    {
        Id = Guid.NewGuid();
        LeftOperand = leftOperand;
        RightOperand = rightOperand;
        Operation = operation;

        Answer = Operation.Act(leftOperand, rightOperand);
    }

    public ResolvedExercise Resolve(double userAnswer, TimeSpan elapsedTime)
        => new(userAnswer, elapsedTime, this);
}
