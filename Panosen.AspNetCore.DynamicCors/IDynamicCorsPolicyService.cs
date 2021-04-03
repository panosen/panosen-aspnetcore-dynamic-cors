using Microsoft.AspNetCore.Cors.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Panosen.AspNetCore.DynamicCors
{
    /// <summary>
    /// IDynamicCorsPolicyService
    /// </summary>
    public interface IDynamicCorsPolicyService
    {
        /// <summary>
        /// IsAllowedAsync
        /// </summary>
        Task<bool> IsAllowedAsync(string origin);

        /// <summary>
        /// BuildCorsPolicy
        /// </summary>
        Task<CorsPolicy> BuildCorsPolicy(string origin);
    }
}
