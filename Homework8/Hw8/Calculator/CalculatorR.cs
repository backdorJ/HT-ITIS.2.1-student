namespace Hw8.Calculator;

public static class CalculatorR
{
    public static double CalculateByOperation(double val1, Operation operation, double val2, ICalculator calculate)
    {
        try
        {
            return operation switch
            {
                Operation.Plus => calculate.Plus(val1, val2),
                Operation.Minus => calculate.Minus(val1, val2),
                Operation.Multiply => calculate.Multiply(val1, val2),
                Operation.Divide => calculate.Divide(val1, val2),
                Operation.Invalid => throw new InvalidOperationException(Messages.InvalidOperationMessage),
            };
        }
        catch (ArgumentException)
        {
            throw new ArgumentException(Messages.InvalidOperationMessage);
        }
    }
}