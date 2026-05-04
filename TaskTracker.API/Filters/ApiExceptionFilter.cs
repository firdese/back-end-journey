using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaskTracker.Application.Exceptions;

namespace TaskTracker.API.Filters;

public sealed class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ForbiddenResourceAccessException)
        {
            context.Result = new ForbidResult();
            context.ExceptionHandled = true;
        }
    }
}
