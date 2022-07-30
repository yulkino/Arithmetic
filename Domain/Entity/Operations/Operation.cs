namespace Domain.Entity.Operations;

public abstract class Operation
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
        public override string ToString() => "+";
        public override double Act(double leftOperand, double rightOperand) 
            => leftOperand + rightOperand;
    }

    private class SubtractionOperation : Operation
    {
        public override string ToString() => "-";
        public override double Act(double leftOperand, double rightOperand)
            => leftOperand - rightOperand;
    }

    private class MultiplicationOperation : Operation
    {
        public override string ToString() => "×";
        public override double Act(double leftOperand, double rightOperand)
            => leftOperand * rightOperand;
    }

    private class DivisionOperation : Operation
    {
        public override string ToString() => "÷";
        public override double Act(double leftOperand, double rightOperand)
            => leftOperand / rightOperand;
    }
}
