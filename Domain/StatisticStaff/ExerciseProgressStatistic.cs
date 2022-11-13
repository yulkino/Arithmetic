namespace Domain.StatisticStaff;

/// <summary>
/// Represents a node of progress in exercises resolve duration
/// </summary>
/// <param name="X">Exercises resolve date</param>
/// <param name="Y">Exercises resolve average duration on date <seealso cref="X"/></param>
public sealed record ExerciseProgressStatistic(DateTime X, double Y) : IStatisticElement<DateTime, double>;