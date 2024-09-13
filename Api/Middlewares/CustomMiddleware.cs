using System.Diagnostics;

namespace LearningASP.NewFolder;

public class CustomMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        Debug.WriteLine("Custom middleware");

        await next.Invoke(context);
    }
}