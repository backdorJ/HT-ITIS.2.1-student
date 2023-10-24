using System.Reflection;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.HtmlCreateServices;

public static class CreateFormService
{
    public static IHtmlContent CreateHtml(PropertyInfo prop, object obj)
    {
        var divContainer = new TagBuilder("div");

        if (!prop.PropertyType.IsEnum)
            ValidateExtensions.GroupLabelAndInput(prop, divContainer);
        else
            ValidateExtensions.GroupLabelAndSelection(prop, divContainer);

        ValidateExtensions.ValidateProperty(prop, obj, divContainer);
        return divContainer;
    }
}