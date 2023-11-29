using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Hw11.ErrorMessages;

namespace Hw11.Expressions.Visitor;

[ExcludeFromCodeCoverage]
public class ExpressionsVisitor
{
     /// <summary>
    /// Получение выражения
    /// </summary>
    /// <param name="expression">Выражение</param>
    /// <returns>Выражение</returns>
    public static async Task<double> VisitExpression(Expression expression)
    {
        var result = await Visit((dynamic)expression);
        return result;
    }

    private static async Task<double> Visit(ConstantExpression expression)
    {
        var value = (double)expression.Value!;
        return await Task.Run(() => value);
    }
    
    private static async Task<double> Visit(UnaryExpression expression)
    {
        var value = CompileUnaryAsync(expression).Result;
        return await Task.Run(() => value);
    }

    private static async Task<double> CompileUnaryAsync(Expression expression)
    {
        await Task.Delay(1000);

        var expressionCompiled = Task.Run(() => Expression.Lambda<Func<double>>(expression).Compile().Invoke());

        return await expressionCompiled;
    }
    
    /// <summary>
    /// Получение операции 
    /// </summary>
    /// <param name="node">Бинарное выражение</param>
    /// <returns>Операция выражения</returns>
    private static async Task<double> Visit(BinaryExpression node)
    {
        var values = CompileAsync(node.Left, node.Right).Result;
        var expression = GetExpressionByType(node.NodeType, values);
        var result = Expression.Lambda<Func<double>>(expression).Compile().Invoke();
        return await Task.Run(() => result);
    }

    /// <summary>
    /// Компиляция лябд
    /// </summary>
    /// <param name="left">Левое выражение</param>
    /// <param name="right">Правое выражение</param>
    /// <returns>Массив чисел</returns>
    private static async Task<double[]> CompileAsync(Expression left, Expression right)
    {
        await Task.Delay(1000);
        
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