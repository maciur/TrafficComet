using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Runtime.CompilerServices;
using TrafficComet.Abstracts;
using TrafficComet.Abstracts.Accessors;
using TrafficComet.Abstracts.Consts;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Abstracts.Readers;
using TrafficComet.Core.Abstracts.Configurations;
using TrafficComet.Core.Configs;
using TrafficComet.Core.Extensions;
using TrafficComet.Core.Factories;
using TrafficComet.Core.Generators;
using TrafficComet.Core.Middlewares;
using TrafficComet.Core.Readers;

[assembly: InternalsVisibleTo("TrafficComet.Core.Tests")]
[assembly: InternalsVisibleTo("TrafficComet.Splunk.LogWriter")]

namespace TrafficComet.Core
{
    public static class TrafficCometInstaller
    {
        public static IServiceCollection AddTrafficComet(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddTrafficComet(configuration, false);
        }

        public static IServiceCollection AddTrafficComet(this IServiceCollection services, IConfiguration configuration,
            bool readTraceIdAndClientIfFromHeaders)
        {
            services.Configure<TrafficCometMiddlewareConfig>(configuration.GetSection(ConfigurationSelectors.ROOT,
                ConfigurationSelectors.MIDDLEWARE, ConfigurationSelectors.MIDDLEWARE_ROOT));

            services.Configure<RequestReadMiddlewareConfig>(configuration
                .GetSection(ConfigurationSelectors.ROOT, ConfigurationSelectors.MIDDLEWARE, ConfigurationSelectors.MIDDLEWARE_REQUEST));

            services.Configure<ResponseReadMiddlewareConfig>(configuration
                .GetSection(ConfigurationSelectors.ROOT, ConfigurationSelectors.MIDDLEWARE, ConfigurationSelectors.MIDDLEWARE_RESPONSE));

            services.Configure<ClientIdGeneratorConfig>(configuration
                .GetSection(ConfigurationSelectors.ROOT, ConfigurationSelectors.GENERATOR, ConfigurationSelectors.GENERATOR_CLIENT_ID));

            services.Configure<TraceIdGeneratorConfig>(configuration
                .GetSection(ConfigurationSelectors.ROOT, ConfigurationSelectors.GENERATOR, ConfigurationSelectors.GENERATOR_TRACE_ID));

            services.AddTransient<IClientIdGeneratorConfiguration, ClientIdGeneratorConfiguration>();
            services.AddTransient<ITraceIdGeneratorConfiguration, TraceIdGeneratorConfiguration>();

            //Accessors
            services.AddTransient<ITrafficCometMiddlewaresAccessor, TrafficCometMiddlewaresAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Readers
            services.TryAddTransient<IIpAddressReader, IpAddressReader>();
            services.TryAddTransient<IStringBodyReader, JsonStreamReader>();
            services.TryAddTransient<IDynamicBodyReader, DynamicBodyReader>();
            services.TryAddTransient<IMachineNameReader, MachineNameReader>();

            //Generators
            if (readTraceIdAndClientIfFromHeaders)
            {
                services.TryAddTransient<IClientIdGenerator, HttpHeaderClientIdGenerator>();
                services.TryAddTransient<ITraceIdGenerator, HttpHeaderTraceIdGenerator>();
            }
            else
            {
                services.TryAddTransient<IClientIdGenerator, CookieClientIdGenerator>();
                services.TryAddTransient<ITraceIdGenerator, TraceIdGenerator>();
            }

            //Factories
            services.TryAddTransient<IServerLogFactory, ServiceLogFactory>();
            services.TryAddTransient<IClientLogFactory, ClientLogFactory>();
            services.TryAddTransient<ITrafficLogFactory, TrafficLogFactory>();
            services.TryAddTransient<IRequestLogFactory, RequestLogFactory>();
            services.TryAddTransient<IResponseLogFactory, ResponseLogFactory>();
            services.TryAddTransient<IDatesTrafficFactory, DatesTrafficFactory>();

            return services;
        }

        public static void UseTrafficComet(this IApplicationBuilder app)
        {
            app.UseMiddleware<TrafficCometMiddleware>();
            app.UseMiddleware<RequestReadMiddleware>();
            app.UseMiddleware<ResponseReadMiddleware>();
        }
    }
}