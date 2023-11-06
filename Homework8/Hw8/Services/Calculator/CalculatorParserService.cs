using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Hw8.Calculator;

namespace Hw8.Services;

[ExcludeFromCodeCoverage]
public class CalculatorParserService : ICalculatorParser
{
    public (double val1, double val2) ParseArgs(string val1, string val2)
    {
        var currentVal1 = 0.0;
        var currentVal2 = 0.0;
        
        if (!double.TryParse(val1,NumberStyles.Any, CultureInfo.InvariantCulture,out currentVal1))
            throw new InvalidDataException(Messages.InvalidNumberMessage);
        
        if(!double.TryParse(val2,NumberStyles.Any, CultureInfo.InvariantCulture ,out currentVal2))
            throw new InvalidDataException(Messages.InvalidNumberMessage);

        return (currentVal1, currentVal2);
    }

    public Operation ParseOperation(string operation)
    {
        return operation switch
        {   
            "Plus" => Operation.Plus,
            "Minus" => Operation.Minus,
            "Divide" => Operation.Divide,
            "Multiply" => Operation.Multiply,
            _ => Operation.Invalid
        };
    }
}