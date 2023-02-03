namespace Domain.Entity.SettingsEntities;

public abstract class Difficulty : IEntity
{
    public static readonly Difficulty Easy = new EasyDifficulty();

    public static readonly Difficulty Medium = new MediumDifficulty();

    public static readonly Difficulty Hard = new HardDifficulty();

    public abstract Guid Id { get; protected init; }
    public abstract string Name { get; protected init; }
    public abstract int MaxDigitCount { get; }

    private class EasyDifficulty : Difficulty
    {
        public override Guid Id { get; protected init; } = Guid.Parse("128A2A8D-AE3C-4E5E-AC35-5EC2652353B0");
        public override string Name { get; protected init; } = "Easy";
        public override int MaxDigitCount { get; } = 2;
    }

    private class MediumDifficulty : Difficulty
    {
        public override Guid Id { get; protected init; } = Guid.Parse("36AB3493-2778-4757-AA17-F874DCF6B990");
        public override string Name { get; protected init; } = "Medium";

        public override int MaxDigitCount { get; } = 3;
    }

    private class HardDifficulty : Difficulty
    {
        public override Guid Id { get; protected init; } = Guid.Parse("31B99883-DF8D-4C03-BFC5-8D1BC83D2EEE");
        public override string Name { get; protected init; } = "Hard";

        public override int MaxDigitCount { get; } = 4;
    }
}
