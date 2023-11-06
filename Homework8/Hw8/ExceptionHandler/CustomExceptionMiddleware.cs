using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hw8.ExceptionHandler;

[ExcludeFromCodeCoverage]
public class CustomExceptionMiddleware
{
    private RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandlerExceptionAsync(context, e);
        }
    }

    private async Task HandlerExceptionAsync(HttpContext context, Exception exception)
    {
        var result = "";

        switch (exception)
        {
            case InvalidOperationException invalidOperationException:
                result = invalidOperationException.Message;
                break;
            case InvalidDataException invalidDataException:
                result = invalidDataException.Message;
                break;
            default:
                result = exception.Message;
                break;
        }

        context.Response.ContentType = "text/plain; charset=utf-8";
        await context.Response.WriteAsync(result);
    }
}