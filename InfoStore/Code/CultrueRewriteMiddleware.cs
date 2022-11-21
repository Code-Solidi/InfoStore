using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

using System.Threading.Tasks;

namespace InfoStore.Code
{
    public class CultrueRewriteMiddleware
    {
        private readonly RequestDelegate next;

        public CultrueRewriteMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public Task Invoke(HttpContext context)
        {
            //context.Request.Headers.AcceptLanguage = "bg-BG";
            return this.next(context);
        }
    }

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCultrueRewrite(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CultrueRewriteMiddleware>();
        }
    }
}