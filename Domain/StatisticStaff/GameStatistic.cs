namespace Domain.StatisticStaff;

public sealed record GameStatistic(
    DateTime GameDate,
    int ExerciseCount,
    double GameDuration,
    double CorrectAnswersPercentage);