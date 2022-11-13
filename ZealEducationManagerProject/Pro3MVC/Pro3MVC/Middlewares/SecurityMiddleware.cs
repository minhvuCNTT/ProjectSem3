using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Pro3MVC.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class SecurityMiddleware
    {
        private readonly RequestDelegate _next;

        public SecurityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            var path = httpContext.Request.Path.Value;
            var roleFromSession = httpContext.Session.GetString("role");
            if (path.StartsWith("/admin"))
            {
                if (roleFromSession == null)
                {
                    httpContext.Response.Redirect("/security/login");
                }
                else if (roleFromSession == "student")
                {
                    httpContext.Session.Remove("id");
                    httpContext.Session.Remove("username");
                    httpContext.Session.Remove("fullname");
                    httpContext.Session.Remove("role");
                    httpContext.Response.Redirect("/security/login");
                }
            }
            if (path.StartsWith("/student"))
            {
                if (roleFromSession == "admin")
                {
                    httpContext.Session.Remove("id");
                    httpContext.Session.Remove("username");
                    httpContext.Session.Remove("fullname");
                    httpContext.Session.Remove("role");
                    httpContext.Response.Redirect("/security/login");
                }
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class SecurityMiddlewareExtensions
    {
        public static IApplicationBuilder UseSecurityMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SecurityMiddleware>();
        }
    }
}
