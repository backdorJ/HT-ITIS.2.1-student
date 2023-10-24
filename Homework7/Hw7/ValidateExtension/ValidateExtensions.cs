using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.HtmlCreateServices;

public static class ValidateExtensions
{
    public static string? GetTypeByName(Type type) => type.IsValueType ? "number" : "text";

    public static string SplitCamelCase(string propertyName)
        => string.Join(' ', Regex.Split(propertyName, "(?<!^)(?=[A-Z])"));
    
    public static void ValidateProperty(PropertyInfo prop, object o, TagBuilder divContainer)
    {
        divContainer.InnerHtml.AppendHtml(HtmlComponentsCreateService.ValidatePropertyAndAppendSpan(prop, o, divContainer));
    }

    public static void GroupLabelAndInput(PropertyInfo prop, TagBuilder container)
    {
        container.InnerHtml.AppendHtml(HtmlComponentsCreateService.AppendLabel(prop));
        container.InnerHtml.AppendHtml(HtmlComponentsCreateService.AppendInput(prop));
    }
    
    public static void GroupLabelAndSelection(PropertyInfo prop, TagBuilder container)
    {
        container.InnerHtml.AppendHtml(HtmlComponentsCreateService.AppendLabel(prop));
        container.InnerHtml.AppendHtml(HtmlComponentsCreateService.AppendSelectionContainer(prop));
    }
}