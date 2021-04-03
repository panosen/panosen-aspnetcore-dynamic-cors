using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Extensions.DependencyInjection.Extensions;

using Panosen.AspNetCore.DynamicCors;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// DynamicCorsServiceCollectionExtensions
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// services.TryAdd(ServiceDescriptor.Scoped&lt;IDynamicCorsPolicyService, TDynamicCorsPolicyService&gt;());
        /// </summary>
        public static IServiceCollection AddDynamicCorsScoped<TDynamicCorsPolicyService>(this IServiceCollection services)
            where TDynamicCorsPolicyService : class, IDynamicCorsPolicyService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAdd(ServiceDescriptor.Scoped<IDynamicCorsPolicyService, TDynamicCorsPolicyService>());

            return services;
        }

        /// <summary>
        /// services.TryAdd(ServiceDescriptor.Transient&lt;IDynamicCorsPolicyService, TDynamicCorsPolicyService&gt;());
        /// </summary>
        public static IServiceCollection AddDynamicCorsTransient<TDynamicCorsPolicyService>(this IServiceCollection services)
            where TDynamicCorsPolicyService : class, IDynamicCorsPolicyService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAdd(ServiceDescriptor.Transient<IDynamicCorsPolicyService, TDynamicCorsPolicyService>());

            return services;
        }

        /// <summary>
        /// services.TryAdd(ServiceDescriptor.Singleton&lt;IDynamicCorsPolicyService, TDynamicCorsPolicyService&gt;());
        /// </summary>
        public static IServiceCollection AddDynamicCorsSingleton<TDynamicCorsPolicyService>(this IServiceCollection services)
            where TDynamicCorsPolicyService : class, IDynamicCorsPolicyService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.TryAdd(ServiceDescriptor.Singleton<IDynamicCorsPolicyService, TDynamicCorsPolicyService>());

            return services;
        }

        /// <summary>
        /// AddDynamicCors with built-in DefaultCorsPolicyService
        /// </summary>
        public static IServiceCollection AddDynamicCorsDefault(this IServiceCollection services, Action<DynamicCorsOptions> action = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions<DynamicCorsOptions>();
            services.TryAdd(ServiceDescriptor.Singleton<IDynamicCorsPolicyService, DefaultCorsPolicyService>());

            if (action != null)
            {
                services.Configure(action);
            }

            return services;
        }

        /// <summary>
        /// AddDynamicCors with built-in SnapshotCorsPolicyService
        /// </summary>
        public static IServiceCollection AddDynamicCorsSnapshot(this IServiceCollection services, Action<DynamicCorsOptions> action = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddOptions<DynamicCorsOptions>();
            services.TryAdd(ServiceDescriptor.Scoped<IDynamicCorsPolicyService, SnapshotCorsPolicyService>());

            if (action != null)
            {
                services.Configure(action);
            }

            return services;
        }
    }
}
