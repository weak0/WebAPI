using System.Diagnostics;

namespace WebAPI.Middleware.cs
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        private readonly Stopwatch _stopwatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger) {
        _logger = logger;
        _stopwatch =  new Stopwatch();
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
           _stopwatch.Start();
            await next.Invoke(context);
           _stopwatch.Stop();
           _logger.LogWarning($"To zapytanie trwało: {_stopwatch.ElapsedMilliseconds}ms ", context.Request.Path);
        }
    }
}
