namespace Domain.StatisticStaff;

public interface IStatisticElement<TX, TY>
{
    Guid Id { get; }
    TX X { get; init; }
    TY Y { get; init; }
    int ElementCountStatistic { get; init; }
}