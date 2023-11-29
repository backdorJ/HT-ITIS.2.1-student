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
        var operationExpression = ExpressionTreeConverter.ConvertToTree(polakString);

        await Task.Delay(1000);

        return await ExpressionsVisitor.VisitExpression(operationExpression);
    }
}