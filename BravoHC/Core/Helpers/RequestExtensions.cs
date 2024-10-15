using Microsoft.AspNetCore.Http;

namespace Core.Helpers
{
    public static class RequestExtensions
    {
        public static string BaseUrl(this HttpContext httpContext)
        {
            return "https"+$"://192.168.50.89:7128{httpContext.Request.PathBase}";
        }
    }
}
