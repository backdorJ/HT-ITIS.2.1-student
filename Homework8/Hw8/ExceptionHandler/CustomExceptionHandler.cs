using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Hw8.ExceptionHandler
{
    public static class CustomExceptionHandler
    {
        public static string ErrorHandlingAsync(Exception exception)
        {
            var result = new StringBuilder();

            switch (exception)
            {
                case InvalidOperationException invalidException:
                    result.Append(invalidException.Message);
                    break;
                case InvalidDataException invalidData:
                    result.Append(invalidData.Message);
                    break;
                default:
                    result.Append(exception.Message);
                    break;
            }

            return result.ToString();
        }

    }
}
    