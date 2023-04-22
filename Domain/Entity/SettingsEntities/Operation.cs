namespace Domain.Entity.SettingsEntities;

public sealed class Operation : IEntity
{
    public static readonly Operation Addition = new()
    {
        Id = Guid.Parse("472FE3A7-B8B6-47FA-B74F-07451CD91BC0"), Name = "Addition"
    };

    public static readonly Operation Subtraction = new()
    {
        Id = Guid.Parse("50D78436-3371-421A-8F20-7490BCD58E3A"), Name = "Subtraction"
    };

    public static readonly Operation Multiplication = new()
    {
        Id = Guid.Parse("E713CCFF-8CB3-4437-B8FB-F6B664C0D415"), Name = "Multiplication"
    };

    public static readonly Operation Division = new()
    {
        Id = Guid.Parse("3C0AB4B3-789B-4CB6-B80F-32D5FEFF486B"), Name = "Division"
    };

    private static readonly Dictionary<string, Func<double, double, double>> Action = new()
    {
        ["Addition"] = (left, right) => left + right,
        ["Subtraction"] = (left, right) => left - right,
        ["Multiplication"] = (left, right) => left * right,
        ["Division"] = (left, right) => left / right
    };

    private Operation() { }
    public string Name { get; private init; } = string.Empty;

    public Guid Id { get; private init; }

    public double Act(double leftOperand, double rightOperand)
    {
        return Action[Name](leftOperand, rightOperand);
    }
}