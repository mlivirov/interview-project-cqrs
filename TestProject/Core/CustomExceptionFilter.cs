using System.Buffers;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using TestProject.Domain.Exceptions;

namespace TestProject.Core
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new JsonResult(new
            {
                Message = context.Exception.Message
            }) {
                StatusCode = GetStatusCode(context)
            };
            context.ExceptionHandled = true;
        }

        private int GetStatusCode(ExceptionContext context)
        {
            if (context.Exception is NotFoundException)
            {
                return (int)HttpStatusCode.NotFound;
            }
            else if (context.Exception is ResultNotReadyException)
            {
                return (int)HttpStatusCode.InternalServerError + 1;
            }
            else
            {
                return (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}