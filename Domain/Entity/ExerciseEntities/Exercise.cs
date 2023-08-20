using Domain.Entity.SettingsEntities;

namespace Domain.Entity.ExerciseEntities;

public class Exercise : IEntity
{
    public Exercise(double leftOperand, double rightOperand, Operation operation, DateTime startTime)
    {
        Id = Guid.NewGuid();
        LeftOperand = leftOperand;
        RightOperand = rightOperand;
        Operation = operation;

        StartTime = startTime;

        Answer = Operation.Act(leftOperand, rightOperand);
    }

    private Exercise() { }
    public double LeftOperand { get; }
    public double RightOperand { get; }
    public Operation Operation { get; }
    public double Answer { get; }
    public DateTime StartTime { get; }
    public Guid Id { get; }

    public ResolvedExercise Resolve(double userAnswer, DateTime endTime)
    {
        return new ResolvedExercise(userAnswer, endTime - StartTime, this);
    }
}