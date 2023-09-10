namespace Domain.StatisticStaff;

public interface IStatisticElement<TX, TY>
{
    Guid Id { get; }
    TX X { get; init; }
    TY Y { get; }
    int ElementCountStatistic { get; }

    IStatisticElement<TX, TY> UpdateAverageDuration(TimeSpan newAverageDuration, int newElementCount);
}