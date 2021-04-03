using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Builder;

using Panosen.AspNetCore.DynamicCors;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// DynamicCorsMiddlewareExtensions
    /// </summary>
    public static class DynamicCorsMiddlewareExtensions
    {
        /// <summary>
        /// UseDynamicCors
        /// </summary>
        public static IApplicationBuilder UseDynamicCors(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<DynamicCorsMiddleware>();
        }
    }
}
