using System.Diagnostics.CodeAnalysis;
using Hw10.Dto;
using Hw10.ExpressionParsers;
using Hw10.Expressions.ExpressionConvert;
using Hw10.Expressions.Visitor;
using Hw10.ExpressionValidate;

namespace Hw10.Services.MathCalculator;

[ExcludeFromCodeCoverage]
public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            ExpressionValidates.Validate(expression ?? string.Empty);
            
            var expressionOfPostfix = ParserExpression.InfixToPostfix(expression!);
            var operationExpression = ExpressionTreeConverter.ConvertToTree(expressionOfPostfix);

            await Task.Delay(1000);
            
            var result = await ApplyExpressionVisitorSettings.CompileAndInvokeDelegate(operationExpression);
            return new CalculationMathExpressionResultDto(result);
        }
        catch (Exception e)
        {
            return new CalculationMathExpressionResultDto(e.Message);
        }       
    }
}