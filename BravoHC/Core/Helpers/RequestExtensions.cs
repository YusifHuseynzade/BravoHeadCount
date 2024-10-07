using Microsoft.AspNetCore.Http;

namespace Core.Helpers
{
    public static class RequestExtensions
    {
        public static string BaseUrl(this HttpContext httpContext)
        {
            return "https"+$"://10.10.30.21:5002{httpContext.Request.PathBase}";
        }
    }
}
