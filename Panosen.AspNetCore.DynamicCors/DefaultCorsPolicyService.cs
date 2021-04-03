using Microsoft.AspNetCore.Cors.Infrastructure;
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
    /// DefaultCorsPolicyService
    /// </summary>
    public class DefaultCorsPolicyService : IDynamicCorsPolicyService
    {
        private readonly ILogger logger;

        private readonly DynamicCorsOptions dynamicCorsOptions;

        /// <summary>
        /// DefaultCorsPolicyService
        /// </summary>
        public DefaultCorsPolicyService(ILogger<DefaultCorsPolicyService> logger, IOptions<DynamicCorsOptions> dynamicCorsOptions)
        {
            this.logger = logger;
            this.dynamicCorsOptions = dynamicCorsOptions.Value;
        }

        /// <summary>
        /// IsAllowedAsync
        /// </summary>
        public Task<bool> IsAllowedAsync(string origin)
        {
            logger.LogInformation("you may implement IDynamicCorsPolicyService by yourself.");

            if (dynamicCorsOptions.ForbiddenOrigins != null && dynamicCorsOptions.ForbiddenOrigins.Count > 0)
            {
                logger.LogDebug($"ForbiddenOrigins = {string.Join(";", dynamicCorsOptions.ForbiddenOrigins)}");
            }

            if (dynamicCorsOptions.GrantedOrigins != null && dynamicCorsOptions.GrantedOrigins.Count > 0)
            {
                logger.LogDebug($"GrantedOrigins = {string.Join(";", dynamicCorsOptions.GrantedOrigins)}");
            }

            if (dynamicCorsOptions.ForbiddenOrigins != null && dynamicCorsOptions.ForbiddenOrigins.Any(v => v.Equals(origin, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.FromResult(false);
            }

            if (dynamicCorsOptions.GrantedOrigins != null && dynamicCorsOptions.GrantedOrigins.Any(v => v.Equals(origin, StringComparison.OrdinalIgnoreCase)))
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        /// <summary>
        /// BuildCorsPolicy
        /// </summary>
        public Task<CorsPolicy> BuildCorsPolicy(string origin)
        {
            var policy = new CorsPolicyBuilder()
                .WithOrigins(origin)
                .AllowAnyHeader()
                .AllowAnyMethod();

            if (dynamicCorsOptions.PreflightCacheDuration != null)
            {
                policy.SetPreflightMaxAge(dynamicCorsOptions.PreflightCacheDuration.Value);
            }

            return Task.FromResult(policy.Build());
        }
    }
}
