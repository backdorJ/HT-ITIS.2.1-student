using System.Linq.Expressions;
using Hw9.Expressions.Visitor;

namespace Hw9.ExpressionValidate;

/// <summary>
/// Класс для настроек
/// </summary>
public static class ApplyExpressionVisitorSettings
{
    /// <summary>
    /// Применения VisitExpression
    /// </summary>
    /// <param name="expression">Выражение</param>
    /// <returns>Число</returns>
    public static async Task<double> Apply(Expression expression)
        => Expression.Lambda<Func<double>>(
            await ExpressionsVisitor.VisitExpression(expression)).Compile().Invoke();
}