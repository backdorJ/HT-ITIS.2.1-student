using System.Linq.Expressions;

namespace Hw11.Expressions.Visitor;

/// <summary>
/// Класс для настроек
/// </summary>
public static class ApplyExpressionVisitorDispatcher
{
    /// <summary>
    /// Компилирует и вызывает делегат от BinaryExpression
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static async Task<double> Visit(BinaryExpression expression)
        => Expression.Lambda<Func<double>>(await ExpressionsVisitor.VisitExpressionAsync(expression))
            .Compile()
            .Invoke();

    /// <summary>
    /// Компилирует и вызывает делегат от ConstantExpression
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static async Task<double> Visit(ConstantExpression expression)
        => Expression.Lambda<Func<double>>(await ExpressionsVisitor.VisitExpressionAsync(expression))
            .Compile()
            .Invoke();

}