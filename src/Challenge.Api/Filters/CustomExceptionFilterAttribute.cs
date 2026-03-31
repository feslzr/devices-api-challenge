using Challenge.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Challenge.Api.Filters;

/// <summary>
/// Custom exception filter class
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    /// <summary>
    /// Exception handling method
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task OnExceptionAsync(ExceptionContext context)
    {
        context.Result = new JsonResult(new ErrorResponse(context.Exception.Message))
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };

        context.ExceptionHandled = true;

        return base.OnExceptionAsync(context);
    }
}