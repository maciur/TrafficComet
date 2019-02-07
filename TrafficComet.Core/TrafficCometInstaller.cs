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
namespace TrafficComet.Core
{
    public static class TrafficCometInstaller
    {
        public static IServiceCollection AddTrafficComet(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddTrafficComet(configuration, false);
        }

        public static IServiceCollection AddTrafficComet(this IServiceCollection services, IConfiguration configuration, bool readTraceIdAndClientIfFromHeaders)
        {
            //Configurations
            services.AddOptions();

            services.Configure<TrafficCometMiddlewareConfig>(configuration
                .GetSection(ConfigurationSelectors.ROOT, ConfigurationSelectors.MIDDLEWARE, ConfigurationSelectors.MIDDLEWARE_ROOT));

            services.Configure<RequestReadMiddleware>(configuration
                .GetSection(ConfigurationSelectors.ROOT, ConfigurationSelectors.MIDDLEWARE, ConfigurationSelectors.MIDDLEWARE_REQUEST));

            services.Configure<ResponseReadMiddleware>(configuration
                .GetSection(ConfigurationSelectors.ROOT, ConfigurationSelectors.MIDDLEWARE, ConfigurationSelectors.MIDDLEWARE_RESPONSE));

            services.Configure<ClientIdGeneratorConfig>(configuration
                .GetSection(ConfigurationSelectors.ROOT, ConfigurationSelectors.GENERATOR, ConfigurationSelectors.GENERATOR_CLIENT_ID));

            services.Configure<TraceIdGeneratorConfig>(configuration
                .GetSection(ConfigurationSelectors.ROOT, ConfigurationSelectors.GENERATOR, ConfigurationSelectors.GENERATOR_TRACE_ID));

            services.TryAddTransient<IClientIdGeneratorConfiguration, ClientIdGeneratorConfiguration>();
            services.TryAddTransient<ITraceIdGeneratorConfiguration, TraceIdGeneratorConfiguration>();

            //Accessors
            services.AddScoped<ITrafficCometMiddlewaresAccessor, TrafficCometMiddlewaresAccessor>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Readers
            services.TryAddTransient<IIpAddressReader, IpAddressReader>();
            services.TryAddTransient<IStringBodyReader, JsonStreamReader>();
            services.TryAddTransient<IDynamicBodyReader, DynamicBodyReader>();
            services.TryAddTransient<IMachineNameReader, MachineNameReader>();

            //Generators
            if (readTraceIdAndClientIfFromHeaders)
            {
                services.TryAddScoped<IClientIdGenerator, HttpHeaderClientIdGenerator>();
                services.TryAddScoped<ITraceIdGenerator, HttpHeaderTraceIdGenerator>();
            }
            else
            {
                services.TryAddScoped<IClientIdGenerator, CookieClientIdGenerator>();
                services.TryAddScoped<ITraceIdGenerator, TraceIdGenerator>();
            }

            //Factories
            services.TryAddScoped<IServerLogFactory, ServiceLogFactory>();
            services.TryAddScoped<IClientLogFactory, ClientLogFactory>();
            services.TryAddScoped<ITrafficLogFactory, TrafficLogFactory>();
            services.TryAddScoped<IRequestLogFactory, RequestLogFactory>();
            services.TryAddScoped<IResponseLogFactory, ResponseLogFactory>();
            services.TryAddScoped<IDatesTrafficFactory, DatesTrafficFactory>();

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