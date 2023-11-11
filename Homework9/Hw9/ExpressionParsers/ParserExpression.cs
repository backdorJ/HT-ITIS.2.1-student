using System.Text.RegularExpressions;
using Hw9.ErrorMessages;
using Hw9.RegulareExpressions;

namespace Hw9;

/// <summary>
/// Класс парсера
/// </summary>
public class ParserExpression
{
    /// <summary>
    /// Приоритет операторов
    /// </summary>
    private static Dictionary<string, int> OperatorPriority
        => new()
    {
        { "(", 0 },
        { ")", 0 },
        { "+", 1 },
        { "-", 1 },
        { "*", 2 },
        { "/", 2 }
    };

    /// <summary>
    /// Получение строки в виде польской записи
    /// </summary>
    /// <param name="expression">Строка с варажением</param>
    /// <returns></returns>
    public static string GetPolakString(string expression) 
        => InfixToPostfix(expression);

    #region Алгоритм преобзразования строки в польскую запись
    
    /// <summary>
    /// Преобразовать обычное выражение в польскую
    /// </summary>
    /// <param name="infixExpression">Выражение</param>
    /// <returns>Польская запись</returns>
    private static string InfixToPostfix(string infixExpression)
    {
        var ops = new Stack<string>();
        var polish = new Stack<string>();
        var inputSplited = RegularExpressionTemplates.Delimiters
            .Split(infixExpression.Replace(" ", ""));
        
        var lastTokenIsOp = true;
        for (var i = 0; i < inputSplited.Length; i++)
        {
            var token = inputSplited[i];
            if (token.Length == 0) continue;
            if (RegularExpressionTemplates.Numbers.IsMatch(token))
            {
                polish.Push(token);
                lastTokenIsOp = false;
                continue;
            }
            if (token == "-" && lastTokenIsOp)
            {
                polish.Push(token + inputSplited[++i]);
                lastTokenIsOp = false;
                continue;
            }
            switch (token)
            {
                case "(":
                    ops.Push(token);
                    lastTokenIsOp = true;
                    continue;
                case ")":
                {
                    while (ops.Peek() != "(")
                        PushOperation(ops, polish);
                    ops.Pop();
                    lastTokenIsOp = false;
                    continue;
                }
            }

            while (ops.Count > 0 && OperatorPriority[token] <= OperatorPriority[ops.Peek()])
                PushOperation(ops, polish);
            
            ops.Push(token);
            lastTokenIsOp = true;
        }

        while (ops.Count > 0)
            PushOperation(ops, polish);

        return polish.Pop();
    }
    
    /// <summary>
    /// Вставка операций и значений в стек польской записи
    /// </summary>
    /// <param name="operations">Операции</param>
    /// <param name="polish">Стек польской записи</param>
    private static void PushOperation(Stack<string> operations, Stack<string> polish)
    {
        var op = operations.Pop();
        var val1 = polish.Pop();
        var val2 = polish.Pop();
        polish.Push($"{val2} {val1} {op}");
    }
    
    #endregion
}