namespace Domain.Entity;

public abstract class Operation : IEntity
{
    public static readonly Operation Addition = new AdditionOperation();

    public static readonly Operation Subtraction = new SubtractionOperation();

    public static readonly Operation Multiplication = new MultiplicationOperation();

    public static readonly Operation Division = new DivisionOperation();

    public Guid Id { get; internal set; }
    public abstract override string ToString();
    public abstract double Act(double leftOperand, double rightOperand);

    private class AdditionOperation : Operation
    {
        public override double Act(double leftOperand, double rightOperand)
            => leftOperand + rightOperand;
        public override string ToString() => "+";

    }

    private class SubtractionOperation : Operation
    {
        public override double Act(double leftOperand, double rightOperand)
            => leftOperand - rightOperand;
        public override string ToString() => "-";

    }

    private class MultiplicationOperation : Operation
    {
        public override double Act(double leftOperand, double rightOperand)
            => leftOperand * rightOperand;
        public override string ToString() => "×";
    }

    private class DivisionOperation : Operation
    {
        public override double Act(double leftOperand, double rightOperand)
            => leftOperand / rightOperand;
        public override string ToString() => "÷";
    }
}
