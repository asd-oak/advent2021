namespace advent2021;
public static class Helpers
{
    public static string GetControllerName(this IHttpContextAccessor hca)
    {
        return hca!.HttpContext!.Request.RouteValues["controller"]!.ToString()!.ToLowerInvariant();
    }
}