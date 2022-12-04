namespace Domain.Entity.SettingsEntities;

public abstract class Operation : IEntity
{
    public static readonly Operation Addition = new AdditionOperation();

    public static readonly Operation Subtraction = new SubtractionOperation();

    public static readonly Operation Multiplication = new MultiplicationOperation();

    public static readonly Operation Division = new DivisionOperation();

    public abstract Guid Id { get; protected init; }
    public abstract override string ToString();
    public abstract double Act(double leftOperand, double rightOperand);

    private class AdditionOperation : Operation
    {
        public override Guid Id { get; protected init; } = Guid.Parse("472FE3A7-B8B6-47FA-B74F-07451CD91BC0");

        public override double Act(double leftOperand, double rightOperand)
            => leftOperand + rightOperand;
        public override string ToString() => "+";

    }

    private class SubtractionOperation : Operation
    {
        public override Guid Id { get; protected init; } = Guid.Parse("50D78436-3371-421A-8F20-7490BCD58E3A");

        public override double Act(double leftOperand, double rightOperand)
            => leftOperand - rightOperand;
        public override string ToString() => "-";

    }

    private class MultiplicationOperation : Operation
    {
        public override Guid Id { get; protected init; } = Guid.Parse("E713CCFF-8CB3-4437-B8FB-F6B664C0D415");

        public override double Act(double leftOperand, double rightOperand)
            => leftOperand * rightOperand;
        public override string ToString() => "×";
    }

    private class DivisionOperation : Operation
    {
        public override Guid Id { get; protected init; } = Guid.Parse("3C0AB4B3-789B-4CB6-B80F-32D5FEFF486B");

        public override double Act(double leftOperand, double rightOperand)
            => leftOperand / rightOperand;
        public override string ToString() => "÷";
    }
}
