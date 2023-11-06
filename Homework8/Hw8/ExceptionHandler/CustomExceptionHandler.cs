using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hw8.ExceptionHandler
{
    [ExcludeFromCodeCoverage]
    public static class CustomExceptionHandler
    {
        public static string ErrorHandlingAsync(Exception exception)
        {
            return exception switch
            {
                InvalidOperationException invalidException => invalidException.Message,
                InvalidDataException invalidData => invalidData.Message,
                _ => exception.Message
            };
        }

    }
}
    