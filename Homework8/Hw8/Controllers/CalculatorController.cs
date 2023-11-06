using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hw8.Calculator;
using Hw8.ExceptionHandler;
using Microsoft.AspNetCore.Mvc;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    private readonly ICalculatorParser _calculatorParser;
    
    [ExcludeFromCodeCoverage]
    public CalculatorController(ICalculatorParser calculatorParser)
    {
        _calculatorParser = calculatorParser;
    }

    public ActionResult<double> Calculate([FromServices] ICalculator calculator,
        string val1,
        string operation,
        string val2)
    {
        var (firstValue, secondValue) = _calculatorParser.ParseArgs(val1, val2);
        var parsedOperation = _calculatorParser.ParseOperation(operation);
        return parsedOperation switch
        {
            Operation.Plus => calculator.Plus(firstValue, secondValue),
            Operation.Multiply => calculator.Multiply(firstValue, secondValue),
            Operation.Divide => calculator.Divide(firstValue, secondValue),
            Operation.Minus => calculator.Minus(firstValue, secondValue),
            Operation.Invalid => throw new InvalidOperationException(Messages.InvalidOperationMessage),
            _ => throw new ArgumentException(Messages.InvalidOperationMessage)
        };
    }

    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}