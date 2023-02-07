namespace Domain.StatisticStaff;

public sealed record GameStatistic(
    Guid Id,
    DateTime GameDate,
    int ExerciseCount,
    double GameDuration,
    double CorrectAnswersPercentage);