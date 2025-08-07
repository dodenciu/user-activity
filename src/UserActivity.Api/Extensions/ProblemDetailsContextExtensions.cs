using Microsoft.AspNetCore.Diagnostics;
using UserActivity.Application;

namespace UserActivity.Api.Extensions;

public static class ProblemDetailsContextExtensions
{
    internal static bool HandleFluentValidation(this ProblemDetailsContext context)
    {
        Exception? exception = context.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        if (exception is not ValidationException validationException)
        {
            return false;
        }

        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        context.ProblemDetails.Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1";
        context.ProblemDetails.Title = "Validation Failed";
        context.ProblemDetails.Status = StatusCodes.Status400BadRequest;
        context.ProblemDetails.Detail = "One or more validation errors occured.";
        context.ProblemDetails.Extensions["errors"] = validationException.Errors;

        return true;
    }
}
