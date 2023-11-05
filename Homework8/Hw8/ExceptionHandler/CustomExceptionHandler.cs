using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hw8.Middleware
{
    public class CustomExceptionHandler
    {
        public static string ErrorHandlingAsync(Exception exception)
        {
            var result = string.Empty;

            switch (exception)
            {
                case InvalidOperationException invalidException:
                    result = invalidException.Message;
                    break;
                case InvalidDataException invalidData:
                    result = invalidData.Message;
                    break;
                default:
                    result = exception.Message;
                    break;
            }

            return result;
        }

    }
}
    