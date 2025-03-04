using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.HtmlCreateServices;

public static class HtmlComponentsCreateService
{
    public static IHtmlContent AppendSelectionContainer(PropertyInfo prop)
    {
        var selectContainer = new TagBuilder("select");
        selectContainer.Attributes.Add("id", prop.Name);
        
        foreach (var value in prop.PropertyType.GetEnumValues())
        {
            var optionContainer = new TagBuilder("option");
            optionContainer.Attributes.Add("value", $"{value}");
            optionContainer.InnerHtml.AppendHtml($"{value}");
            selectContainer.InnerHtml.AppendHtml(optionContainer);
        }

        return selectContainer;
    }
    
    public static IHtmlContent AppendInput(PropertyInfo prop)
    {
        var inputContainer = new TagBuilder("input");
        inputContainer.Attributes.Add("type", ValidateExtensions.GetTypeByName(prop.PropertyType));
        inputContainer.Attributes.Add("name", prop.Name);
        inputContainer.Attributes.Add("id", prop.Name);
        
        return inputContainer;
    }
    
    public static IHtmlContent AppendLabel(PropertyInfo prop)
    {
        var labelContainer = new TagBuilder("label");

        var displayName = prop
            .GetCustomAttributes<DisplayAttribute>()
            .FirstOrDefault()?
            .Name;

        if (displayName == null)
            labelContainer.InnerHtml.AppendHtml(ValidateExtensions.SplitCamelCase(prop.Name));
        else
            labelContainer.InnerHtml.AppendHtml(displayName);
        
        labelContainer.Attributes.Add("for", prop.Name);
        return labelContainer;
    }
    
    public static IHtmlContent ValidatePropertyAndAppendSpan(PropertyInfo prop, object model, TagBuilder container)
    {
        var spanContainer = new TagBuilder("span");
        spanContainer.InnerHtml.AppendHtml(string.Empty);
        if (model == null) return spanContainer;

        foreach (ValidationAttribute validationAttribute in prop.GetCustomAttributes(typeof(ValidationAttribute), true))
        {
            if (validationAttribute.IsValid(prop.GetValue(model)))
                continue;
            
            spanContainer.InnerHtml.AppendHtml(validationAttribute.ErrorMessage!);
            return spanContainer;
        }

        return spanContainer;
    }
}