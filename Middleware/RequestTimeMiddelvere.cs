using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddelvere : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddelvere> _logger;
        private Stopwatch _stopWatch;

        public RequestTimeMiddelvere(ILogger<RequestTimeMiddelvere> logger)
        {
            _logger = logger;
            _stopWatch = new Stopwatch();
        }


        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopWatch.Start();
            await next.Invoke(context);
            _stopWatch.Stop();

            var elapsedMilisecounds = _stopWatch.ElapsedMilliseconds;
            if(elapsedMilisecounds/1000 > 4)
            {
                var message = $"Request: {context.Request.Method} at {context.Request.Path} took {elapsedMilisecounds}";
                _logger.LogInformation(message);
            }
        }
    }
}
