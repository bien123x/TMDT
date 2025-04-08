using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ThuongMaiDienTu.Middleware
{
    public class BlockDirectAccessMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BlockDirectAccessMiddleware> _logger;

        public BlockDirectAccessMiddleware(RequestDelegate next, ILogger<BlockDirectAccessMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check if there is no Referer (user entered URL directly) and the request is not for the home page
            if (string.IsNullOrEmpty(context.Request.Headers["Referer"]) &&
                !context.Request.Path.StartsWithSegments("/Home/Index"))
            {
                _logger.LogWarning("Direct access attempt detected. Redirecting to home page.");
                context.Response.Redirect("/Home/Index"); // Redirect to home page
                return;
            }

            await _next(context);
        }
    }
}
