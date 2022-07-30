namespace Domain.Entity.Difficulties;

public abstract class Difficulty
{
    public static readonly Difficulty Easy = new EasyDifficulty();

    public static readonly Difficulty Medium = new MediumDifficulty();

    public static readonly Difficulty Hard = new HardDifficulty();

    public Guid Id { get; set; }

    public abstract int MaxDigitCount { get; }

    private class EasyDifficulty : Difficulty
    {
        public override int MaxDigitCount { get; } = 2;
    }

    private class MediumDifficulty : Difficulty
    {
        public override int MaxDigitCount { get; } = 3;
    }

    private class HardDifficulty : Difficulty
    {
        public override int MaxDigitCount { get; } = 4;
    }
}
