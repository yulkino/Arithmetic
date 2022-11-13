namespace Domain.StatisticStaff;

public interface IStatisticElement<TX, TY>
{
    TX X { get; init; }
    TY Y { get; init; }
}