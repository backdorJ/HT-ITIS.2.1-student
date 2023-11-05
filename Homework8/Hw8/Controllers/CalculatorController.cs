using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hw8.Calculator;
using Hw8.ExceptionHandler;
using Microsoft.AspNetCore.Mvc;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    public ActionResult<double> Calculate([FromServices] ICalculator calculator,
        string val1,
        string operation,
        string val2)
    
    {
        try
        {
            var (firstValue, secondValue) = CalculateParser.ParseArgs(val1, val2);
            var parsedOperation = CalculateParser.ParseOperation(operation);
            return CalculatorR.CalculateByOperation(firstValue, parsedOperation, secondValue, calculator);
        }
        catch (Exception e)
        {
            return Content(CustomExceptionHandler.ErrorHandlingAsync(e));
        }
    }
    
    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}