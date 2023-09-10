using Domain.Entity.SettingsEntities;

namespace Domain.StatisticStaff;

/// <summary>
///     Represents a node of progress in exercises that use specific operation
/// </summary>
public sealed class OperationsStatistic : IStatisticElement<Operation, TimeSpan>
{
    public OperationsStatistic(Operation x, TimeSpan y, int elementCountStatistic)
    {
        Id = Guid.NewGuid();
        X = x;
        Y = y;
        ElementCountStatistic = elementCountStatistic;
    }

    private OperationsStatistic() { }

    public Guid Id { get; }

    /// <summary>Exercises operation</summary>
    public Operation X { get; init; }

    /// <summary>Exercises resolve average duration with exercises that use an operation <seealso cref="X" /></summary>
    public TimeSpan Y { get; private set; }

    /// <summary>The number of elements that participated in the calculation of this node/></summary>
    public int ElementCountStatistic { get; private set; }

    public IStatisticElement<Operation, TimeSpan> UpdateAverageDuration(
        TimeSpan newAverageDuration, int newElementCount)
    {
        Y = newAverageDuration;
        ElementCountStatistic = newElementCount;
        return this;
    }
}