using System.Diagnostics.CodeAnalysis;
using Hw9.ErrorMessages;
using Hw9.RegulareExpressions;

namespace Hw9.ExpressionValidate;

/// <summary>
/// Класс валидации
/// </summary>
[ExcludeFromCodeCoverage]
public static class ExpressionValidates
{
    /// <summary>
    /// Проверка на кореектность выражения
    /// </summary>
    /// <param name="expression">строка с выражением</param>
    /// <exception cref="Exception">Пустая строка</exception>
    public static void Validate(string expression)
    {
        if (string.IsNullOrEmpty(expression))
            throw new Exception(MathErrorMessager.EmptyString);

        if (!AreParenthesesBalanced(expression))
            throw new Exception(MathErrorMessager.IncorrectBracketsNumber);

        ValidateToCorrectCharacter(expression);

        if (!DoesNotStartWithOperator(expression))
            throw new Exception(MathErrorMessager.StartingWithOperation);

        if (!DoesNotEndWithOperator(expression))
            throw new Exception(MathErrorMessager.EndingWithOperation);

        ComplexValidateAllCharacters(expression);
    }

    /// <summary>
    /// Проверяет выражение на допустимые значения
    /// </summary>
    /// <param name="expression">Строковое выражение</param>
    private static void ValidateToCorrectCharacter(string expression)
    {
        foreach (var c in expression.Where(c => 
                     !RegularExpressionTemplates.Numbers.IsMatch(c.ToString())
                     && !new[] { '+', '-', '*', '/', '(', ')', '.', ' ' }.Contains(c)))
            throw new Exception(MathErrorMessager.UnknownCharacterMessage(c));
    }
    
    /// <summary>
    /// Проверка на сбалансированность скобок
    /// </summary>
    /// <param name="input">строка</param>
    /// <returns>Булевое значение</returns>
    private static bool AreParenthesesBalanced(string input)
    {
        var stack = new Stack<char>();

        foreach (var ch in input)
        {
            switch (ch)
            {
                case '(':
                    stack.Push(ch);
                    break;
                case ')' when stack.Count == 0 || stack.Pop() != '(':
                    return false; // Несбалансированные скобки
            }
        }

        return stack.Count == 0; // Сбалансированные скобки, если стек пуст
    }
    
    /// <summary>
    /// Проверка что выражение не начинается с операции
    /// </summary>
    /// <param name="expression">Выражение</param>
    /// <returns>Булевое значение</returns>
    private static bool DoesNotStartWithOperator(string expression) =>
        !string.IsNullOrWhiteSpace(expression) && 
        !new[] { '+', '-', '*', '/' }.Contains(expression[0]);
    
    /// <summary>
    /// Проверка что выражение не кончается на операцию
    /// </summary>
    /// <param name="expression">Выражение</param>
    /// <returns>Булевое значение</returns>
    private static bool DoesNotEndWithOperator(string expression) =>
        !string.IsNullOrWhiteSpace(expression) && 
        !new[] { '+', '-', '*', '/' }.Contains(expression[^1]);

    /// <summary>
    /// Проверка выражения на детали
    /// </summary>
    /// <param name="expression">Выражение</param>
    private static void ComplexValidateAllCharacters(string expression)
    {
        var lastToken = string.Empty;
        var lastTokeOfOperation = true;

        foreach (var token in RegularExpressionTemplates.Delimiters
                     .Split(expression.Replace(" ", ""))
                     .Where(token => !string.IsNullOrEmpty(token)))
        {
            if (RegularExpressionTemplates.Numbers.IsMatch(token))
            {
                ValidateNumber(token);
                lastToken = token;
                lastTokeOfOperation = false;
            }
            else if (IsUnaryMinus(token, lastTokeOfOperation))
            {
                lastToken = token;
                lastTokeOfOperation = false;
            }
            else
            {
                switch (token)
                {
                    case "(":
                        ValidateOpenParenthesis(lastTokeOfOperation, token);
                        lastToken = token;
                        lastTokeOfOperation = true;
                        break;
                    case ")":
                        ValidateCloseParenthesis(lastTokeOfOperation, lastToken);
                        lastToken = token;
                        lastTokeOfOperation = false;
                        break;
                    default:
                        ValidateOperatorAfterParenthesis(lastTokeOfOperation, lastToken, token);
                        ValidateTwoOperatorsInRow(lastTokeOfOperation, lastToken, token);
                        lastToken = token;
                        lastTokeOfOperation = true;
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Проверка на две операции в выражении
    /// </summary>
    /// <param name="lastTokenIsOp">последняя операция булеове значение</param>
    /// <param name="lastToken">последняя операция</param>
    /// <param name="token">текущяя операция</param>
    /// <exception cref="Exception">если две операции стоят подряд</exception>
    private static void ValidateTwoOperatorsInRow(bool lastTokenIsOp, string lastToken, string token)
    {
        if (lastTokenIsOp)
            throw new Exception(MathErrorMessager.TwoOperationInRowMessage(lastToken, token));
    }

    /// <summary>
    /// Проверка что после скобки нет операции
    /// </summary>
    /// <param name="lastTokenIsOp">последняя операция булеове значение</param>
    /// <param name="lastToken">последняя операция</param>
    /// <param name="token">текущяя операция</param>
    /// <exception cref="Exception">если после скобки идет операция</exception>
    private static void ValidateOperatorAfterParenthesis(bool lastTokenIsOp, string lastToken, string token)
    {
        if (lastTokenIsOp && lastToken == "(")
            throw new Exception(MathErrorMessager.InvalidOperatorAfterParenthesisMessage(token));
    }

    /// <summary>
    /// Проверка что после операции не идет скобка
    /// </summary>
    /// <param name="lastTokenIsOp">последняя операция булеове значение</param>
    /// <param name="lastToken">последняя операция</param>
    /// <exception cref="Exception">если это не так</exception>
    private static void ValidateCloseParenthesis(bool lastTokenIsOp, string lastToken)
    {
        if (lastTokenIsOp)
            throw new Exception(MathErrorMessager.OperationBeforeParenthesisMessage(lastToken));
    }

    /// <summary>
    /// Перед закрывающей скобкой стоит только число
    /// </summary>
    /// <param name="lastTokenIsOp">Последняя операция булеове значение</param>
    /// <param name="token">Последняя операция</param>
    /// <exception cref="Exception">Если это не так</exception>
    private static void ValidateOpenParenthesis(bool lastTokenIsOp, string token)
    {
        if (!lastTokenIsOp)
            throw new Exception(MathErrorMessager.OperationBeforeParenthesisMessage(token));
    }
    
    /// <summary>
    /// Проверка на число
    /// </summary>
    /// <param name="token">Операция или число</param>
    /// <exception cref="Exception">Если не число</exception>
    private static void ValidateNumber(string token)
    {
        if (!double.TryParse(token, out _))
            throw new Exception(MathErrorMessager.NotNumberMessage(token));
    }
    
    /// <summary>
    /// Унарный ли минус
    /// </summary>
    /// <param name="token">Число или операция</param>
    /// <param name="lastTokenIsOp">Операция ли</param>
    /// <returns>Булевое значение</returns>
    private static bool IsUnaryMinus(string token, bool lastTokenIsOp)
        => token == "-" && lastTokenIsOp;
}   