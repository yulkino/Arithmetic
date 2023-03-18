namespace Domain.StatisticStaff;

/// <summary>
/// Represents a node of progress in exercises resolve duration
/// </summary>
/// <param name="X">Exercises resolve date</param>
/// <param name="Y">Exercises resolve average duration on date <seealso cref="X"/></param>
/// <param name="ElementCountStatistic">The number of elements that participated in the calculation of this node/></param>
public sealed record ExerciseProgressStatistic(DateTime X, TimeSpan Y, int ElementCountStatistic) : IStatisticElement<DateTime, TimeSpan>;