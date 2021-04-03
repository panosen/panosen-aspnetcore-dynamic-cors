using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Panosen.AspNetCore.DynamicCors
{
    /// <summary>
    /// DynamicCorsOptions
    /// </summary>
    public class DynamicCorsOptions
    {
        /// <summary>
        /// The value to be used in the preflight `Access-Control-Max-Age` response header.
        /// </summary>
        public TimeSpan? PreflightCacheDuration { get; set; }

        /// <summary>
        /// 白名单
        /// </summary>
        public List<string> GrantedOrigins { get; set; }

        /// <summary>
        /// 黑名单
        /// </summary>
        public List<string> ForbiddenOrigins { get; set; }
    }
}
