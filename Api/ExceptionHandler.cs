using System.Net;
using System.Text;

using LearningASP.Services;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LearningASP;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (hanled, statusCode, message) = exception switch
        {
            UserWithEmailExistsException => (true, 409, "User with email already exists"),
            _ => (false, 500, "Internal Server Error")
        };

        if (!hanled)
        {
            return false;
        }

        var problemDetails = new ProblemDetails { Status = statusCode, Detail = message, };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}