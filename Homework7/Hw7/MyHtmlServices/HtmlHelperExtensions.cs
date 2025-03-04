using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Hw7.HtmlCreateServices;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hw7.MyHtmlServices;

public static class HtmlHelperExtensions
{
    public static IHtmlContent MyEditorForModel<TModel>(this IHtmlHelper<TModel> helper)
    {
        var typeModel = helper.ViewData.ModelExplorer.ModelType;
        var model = helper.ViewData.Model;
        var htmlContent = new HtmlContentBuilder();
        
        foreach (var prop in typeModel.GetProperties())
            htmlContent.AppendHtml(CreateFormService.CreateHtml(prop, model!));

        return htmlContent;
    }
    
} 