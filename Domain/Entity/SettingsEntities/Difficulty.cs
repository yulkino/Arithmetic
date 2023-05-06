namespace Domain.Entity.SettingsEntities;

public sealed class Difficulty : IEntity
{
    public static readonly Difficulty Easy = new()
    {
        Id = Guid.Parse("128A2A8D-AE3C-4E5E-AC35-5EC2652353B0"),
        Name = nameof(Easy),
        MaxDigitCount = 2
    };

    public static readonly Difficulty Medium = new()
    {
        Id = Guid.Parse("36AB3493-2778-4757-AA17-F874DCF6B990"),
        Name = nameof(Medium),
        MaxDigitCount = 3
    };

    public static readonly Difficulty Hard = new()
    {
        Id = Guid.Parse("31B99883-DF8D-4C03-BFC5-8D1BC83D2EEE"),
        Name = nameof(Hard),
        MaxDigitCount = 4
    };

    public string Name { get; private init; } = string.Empty;
    public int MaxDigitCount { get; private init; }

    public Guid Id { get; private init; }
}