namespace Domain.Entity.Exercises;

public class ResolvedExercise
{
    public Guid Id { get; init; }
    public double UserAnswer { get; init; }
    public TimeSpan ElapsedTime { get; init; }
    public bool IsCorrect { get; }
    public Exercise Exercise { get; init; }

    internal ResolvedExercise(double userAnswer, TimeSpan elapsedTime, Exercise exercise)
    {
        Id = Guid.NewGuid();
        UserAnswer = userAnswer;
        ElapsedTime = elapsedTime;
        Exercise = exercise;

        IsCorrect = Math.Abs(Exercise.Answer - userAnswer) < 0.01;
    }
}
