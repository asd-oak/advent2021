namespace advent2021.Middleware;

using System.Runtime.InteropServices;
public class RequestAttributeTracingMiddleware
{
    private readonly RequestDelegate _next;

    private readonly OSPlatform[] _platformList = new OSPlatform[] { OSPlatform.FreeBSD, OSPlatform.Linux, OSPlatform.OSX, OSPlatform.Windows };

    public RequestAttributeTracingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext httpContext, Tracer trace)
    {
        using var span = trace.StartSpan("RequestAttributes");

        foreach (var value in _platformList)
        {
            if (RuntimeInformation.IsOSPlatform(value))
            {
                span.SetAttribute("OS", value.ToString());
            }

        }

        await _next(httpContext);
    }
}