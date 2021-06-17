using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Panosen.AspNetCore.DynamicCors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Sample.UsingCustom
{
    public class CustomDynamicCorsPolicyService : IDynamicCorsPolicyService
    {
        private readonly SampleDBContext sampleDBContext;

        public CustomDynamicCorsPolicyService(SampleDBContext sampleDBContext)
        {
            this.sampleDBContext = sampleDBContext;
        }

        public Task<CorsPolicy> BuildCorsPolicy(string origin)
        {
            var originEntity = sampleDBContext.OriginEntityList.FirstOrDefault(v => v.Origin == origin && v.Enabled);
            if (originEntity == null)
            {
                return Task.FromResult<CorsPolicy>(null);
            }

            var policy = new CorsPolicyBuilder()
                .WithOrigins(origin)
                .WithMethods(originEntity.Method);

            return Task.FromResult(policy.Build());
        }

        public Task<bool> IsAllowedAsync(string origin)
        {
            var allowed = sampleDBContext.OriginEntityList.Any(v => v.Origin == origin && v.Enabled);

            return Task.FromResult(allowed);
        }
    }
}
