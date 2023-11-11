using System.Linq.Expressions;
using Hw9.Dto;
using Hw9.ErrorMessages;
using Hw9.Expressions.ExpressionConvert;
using Hw9.Expressions.Visitor;
using Hw9.ExpressionValidate;

namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            ExpressionValidates.Validate(expression ?? "");
            var polakString = ParserExpression.GetPolskyString(expression!);    
            var operationExpression = ExpressionTreeConverter.ConvertToTree(polakString);
            var result = await ApplyExpressionVisitorSettings.Apply(operationExpression);
            
            return new CalculationMathExpressionResultDto(result);
        }
        catch (Exception e)
        {
            return new CalculationMathExpressionResultDto(e.Message);
        }
    }
}