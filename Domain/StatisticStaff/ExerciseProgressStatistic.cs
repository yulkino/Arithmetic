namespace Domain.StatisticStaff;

/// <summary>
///     Represents a node of progress in exercises resolve duration
/// </summary>
public sealed class ExerciseProgressStatistic : IStatisticElement<DateTime, TimeSpan>
{
    public ExerciseProgressStatistic(DateTime x, TimeSpan y, int elementCountStatistic)
    {
        Id = Guid.NewGuid();
        X = x;
        Y = y;
        ElementCountStatistic = elementCountStatistic;
    }

    private ExerciseProgressStatistic() { }

    public Guid Id { get; }

    /// <summary>Exercises resolve date</summary>
    public DateTime X { get; init; }

    /// <summary>Exercises resolve average duration on date <seealso cref="X" /></summary>
    public TimeSpan Y { get; init; }

    /// <summary>The number of elements that participated in the calculation of this node/></summary>
    public int ElementCountStatistic { get; init; }
}