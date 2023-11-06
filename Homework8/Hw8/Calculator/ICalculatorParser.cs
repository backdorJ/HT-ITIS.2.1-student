namespace Hw8.Calculator;

public interface ICalculatorParser
{
    (double val1, double val2) ParseArgs(string val1, string val2);
    Operation ParseOperation(string operation);
}