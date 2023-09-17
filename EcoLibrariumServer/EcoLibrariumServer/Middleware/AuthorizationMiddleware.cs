using EcoLibrariumServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace EcoLibrariumServer.Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationMiddleware(RequestDelegate next)
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

            if (user == null)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return;
            }

            await _next(httpContext);
        }
    }

    public static class AuthorizationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthorizationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthorizationMiddleware>();
        }
    }
}
