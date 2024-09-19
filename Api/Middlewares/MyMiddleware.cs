namespace LearningASP.NewFolder;

public class MyMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await next.Invoke(context);
    }
}