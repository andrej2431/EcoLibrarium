using EcoLibrariumServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace EcoLibrariumServer.Middleware
{
    public class AdminAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string? sessionId = httpContext.Request.Headers["Authorization"];

            var dbContext = httpContext.RequestServices.GetRequiredService<ApiContext>();
            if (!dbContext.Database.EnsureCreated())
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            if (string.IsNullOrEmpty(sessionId))
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            var user = dbContext.Users.FirstOrDefault(u => u.SessionId == sessionId);

            if (user == null || user.Role != "Admin")
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            await _next(httpContext);
        }
    }

    public static class AdminAuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAdminAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AdminAuthorizationMiddleware>();
        }
    }
}
