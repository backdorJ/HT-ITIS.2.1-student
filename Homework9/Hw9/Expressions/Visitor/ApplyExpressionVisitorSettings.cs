using System.Linq.Expressions;
using Hw9.Expressions.Visitor;

namespace Hw9.ExpressionValidate;

/// <summary>
/// Класс для настроек
/// </summary>
public static class ApplyExpressionVisitorSettings
{
    /// <summary>
    /// Комплирует и вызывает делегат из expression
    /// </summary>
    /// <returns>Число</returns>
    public static async Task<double> CompileAndInvokeDelegate(Expression expression)
        => Expression.Lambda<Func<double>>(
            await ExpressionsVisitor.VisitExpression(expression)).Compile().Invoke();
}