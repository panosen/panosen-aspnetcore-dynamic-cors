using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.AspNetCore.DynamicCors
{
    /// <summary>
    /// DynamicCorsMiddleware
    /// </summary>
    public class DynamicCorsMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;
        private readonly ICorsService corsService;

        /// <summary>
        /// DynamicCorsMiddleware
        /// </summary>
        public DynamicCorsMiddleware(RequestDelegate next,
            ILogger<DynamicCorsMiddleware> logger,
            ICorsService corsService)
        {
            this.logger = logger;
            this.next = next;
            this.corsService = corsService;
        }

        /// <summary>
        /// Invoke
        /// </summary>
        public async Task Invoke(HttpContext context)
        {
            var dynamicCorsPolicyService = context.RequestServices.GetService(typeof(IDynamicCorsPolicyService)) as IDynamicCorsPolicyService;
            if (dynamicCorsPolicyService == null)
            {
                logger.LogInformation("DynamicCorsMiddleware ignored because dynamicCorsPolicyService is null.");
                await next(context);
                return;
            }

            var origin = GetCorsOrigin(context.Request);
            if (origin == null)
            {
                await next(context);
                return;
            }

            var isOriginAllowed = await dynamicCorsPolicyService.IsAllowedAsync(origin);
            if (!isOriginAllowed)
            {
                logger.LogDebug($"CORS request from {origin} is NOT allowed.");
                return;
            }

            logger.LogDebug($"CORS request from {origin} is allowed.");
            var policy = await dynamicCorsPolicyService.BuildCorsPolicy(origin);

            var corsResult = corsService.EvaluatePolicy(context, policy);
            if (corsResult.IsPreflightRequest)
            {
                corsService.ApplyResult(corsResult, context.Response);

                context.Response.StatusCode = StatusCodes.Status204NoContent;
                return;
            }

            context.Response.OnStarting(Callback(context), corsResult);

            await next(context);
        }

        private Func<object, Task> Callback(HttpContext context)
        {
            return state =>
            {
                try
                {
                    corsService.ApplyResult(state as CorsResult, context.Response);
                }
                catch (Exception ex)
                {
                    logger.LogWarning("OnResponseStarting", ex);
                }
                return Task.CompletedTask;
            };
        }

        private static string GetCorsOrigin(HttpRequest request)
        {
            var origin = request.Headers["Origin"].FirstOrDefault();
            if (string.IsNullOrEmpty(origin))
            {
                return null;
            }

            var thisOrigin = request.Scheme + "://" + request.Host;
            if (origin == thisOrigin)
            {
                return null;
            }

            return origin;
        }
    }
}
