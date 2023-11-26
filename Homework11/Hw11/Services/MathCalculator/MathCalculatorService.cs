using Hw10.ExpressionParsers;
using Hw11.Expressions.ExpressionConvert;
using Hw11.Expressions.ExpressionValidate;
using Hw11.Expressions.Visitor;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<double> CalculateMathExpressionAsync(string? expression)
    {
        ExpressionValidates.Validate(expression ?? "");
        var polakString = ParserExpression.InfixToPostfix(expression!);    
        dynamic operationExpression = ExpressionTreeConverter.ConvertToTree(polakString);
        var result = await ApplyExpressionVisitorDispatcher.Visit(operationExpression);

        return result;
    }
}