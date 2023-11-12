using System.Text.RegularExpressions;

namespace Hw9.RegulareExpressions;

/// <summary>
/// Класс шаблонов
/// </summary>
public static class RegularExpressionTemplates
{
    /// <summary>
    /// Регулярное выражение на числа
    /// </summary>
    public static readonly Regex Numbers = new(@"^\d+");
    
    /// <summary>
    /// Регулярное выражение на все элементы строкового выражения
    /// </summary>
    public static readonly Regex Delimiters = new("(?<=[-+*/()])|(?=[-+*/()])");
}