using System.Linq.Expressions;

namespace Hw11.Expressions.ExpressionConvert;

/// <summary>
/// Класс конвертации
/// </summary>
public class ExpressionTreeConverter
{
    /// <summary>
    /// Метод конвертации в дерево выражения
    /// </summary>
    /// <param name="expression">строка выражения</param>
    /// <returns>выражение</returns>
    public static Expression ConvertToTree(string expression)
    {
        var stackOfExpression = new Stack<Expression>();
        foreach (var expressionToken in expression.Split())
        {
            if (double.TryParse(expressionToken, out var digit))
            {
                stackOfExpression.Push(Expression.Constant(digit));
                continue;
            }

            var right = stackOfExpression.Pop();
            var left = stackOfExpression.Pop();
            
            var expressionOfOperation = expressionToken switch
            {
                "+" => Expression.Add(left, right),
                "-" => Expression.Subtract(left, right),
                "/" => Expression.Divide(left, right),
                _ => Expression.Multiply(left, right)
            };

            stackOfExpression.Push(expressionOfOperation);
        }

        return stackOfExpression.Pop();
    }
}