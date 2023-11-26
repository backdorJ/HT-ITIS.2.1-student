using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Hw11.ErrorMessages;

namespace Hw11.Expressions.Visitor;

[ExcludeFromCodeCoverage]
public class ExpressionsVisitor : ExpressionVisitor
{
    /// <summary>
    /// Получение выражения
    /// </summary>
    /// <param name="expression">Выражение</param>
    /// <returns>Выражение</returns>
    public static Task<Expression> VisitExpressionAsync(Expression expression) =>
        Task.Run(() => new ExpressionsVisitor().Visit(expression));
    
    /// <summary>
    /// Получение операции 
    /// </summary>
    /// <param name="node">Бинарное выражение</param>
    /// <returns>Операция выражения</returns>
    protected override Expression VisitBinary(BinaryExpression node)
    {
        var expressionValues = CompileAsync(node.Left, node.Right).Result;

        return GetExpressionByType(node.NodeType, expressionValues);
    }

    /// <summary>
    /// Компиляция лябд
    /// </summary>
    /// <param name="left">Левое выражение</param>
    /// <param name="right">Правое выражение</param>
    /// <returns>Массив чисел</returns>
    private static async Task<double[]> CompileAsync(Expression left, Expression right)
    {
        await Task.Delay(2500);
        
        var firstTask = Task.Run(() => Expression.Lambda<Func<double>>(left).Compile().Invoke());
        var secondTask = Task.Run(() => Expression.Lambda<Func<double>>(right).Compile().Invoke());

        return await Task.WhenAll(firstTask, secondTask);
    }

    /// <summary>
    /// Получаем выражение по типу
    /// </summary>
    /// <param name="expressionType">Тип выражения</param>
    /// <param name="expressionValues">Значения выражений</param>
    /// <returns>Выражение операции</returns>
    /// <exception cref="Exception">Деление на ноль</exception>
    private static Expression GetExpressionByType(ExpressionType expressionType, IReadOnlyList<double> expressionValues)
    {
        return expressionType switch
        {
            ExpressionType.Add => Expression.Add(
                Expression.Constant(expressionValues[0]), Expression.Constant(expressionValues[1])),
            ExpressionType.Subtract => Expression.Subtract(
                Expression.Constant(expressionValues[0]), Expression.Constant(expressionValues[1])),
            ExpressionType.Multiply => Expression.Multiply(
                Expression.Constant(expressionValues[0]), Expression.Constant(expressionValues[1])),
            _ => expressionValues[1] <= 0.0
                ? throw new Exception(MathErrorMessager.DivisionByZero)
                : Expression.Divide(
                    Expression.Constant(expressionValues[0]),
                    Expression.Constant(expressionValues[1]))
        };
    }
}