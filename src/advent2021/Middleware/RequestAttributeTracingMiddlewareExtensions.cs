namespace advent2021.Middleware;
public static class RequestAttributeTracingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestAttributeTracing(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAttributeTracingMiddleware>();
    }
}