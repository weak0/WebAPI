using WebAPI.Exepction;

namespace WebAPI.Middleware.cs
{
    public class ErrorHandlingMiddlewarecs : IMiddleware
    {
        private readonly ILogger _logger;

        public ErrorHandlingMiddlewarecs(ILogger<ErrorHandlingMiddlewarecs> logger)
        {
            _logger = logger;

        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundExeption nfex)
            {
                _logger.LogError(nfex, "Source was not found");
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("Source was not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
