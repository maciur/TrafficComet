using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Runtime.CompilerServices;
using TrafficComet.Abstracts;
using TrafficComet.Abstracts.Accessors;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Abstracts.Readers;
using TrafficComet.Abstractss.Factories;
using TrafficComet.Core.Configs;
using TrafficComet.Core.Consts;
using TrafficComet.Core.Factories;
using TrafficComet.Core.Generators;
using TrafficComet.Core.Middlewares;
using TrafficComet.Core.Readers;
using TrafficComet.JsonLogWriter;

[assembly: InternalsVisibleTo("TrafficComet.Core.Tests")]

namespace TrafficComet.Core
{
    public static class TrafficCometInstaller
    {
        private const string RootConfigName = TrafficCometConstValues.RootConfigName;

        public static IServiceCollection AddTrafficComet(this IServiceCollection services, IConfiguration configuration, bool useJsonLogWriter = true)
        {
            return services.AddTrafficComet(configuration, useJsonLogWriter, false);
        }

        public static IServiceCollection AddTrafficComet(this IServiceCollection services, IConfiguration configuration, bool useJsonLogWriter, bool readTraceIdAndClientIfFromHeaders)
        {
            //Configurations
            services.AddOptions();
            services.Configure<TrafficCometMiddlewareConfig>(configuration.GetSection($"{RootConfigName}:Middleware:Root"));
            services.Configure<RequestReadMiddleware>(configuration.GetSection($"{RootConfigName}:Middleware:Request"));
            services.Configure<ResponseReadMiddleware>(configuration.GetSection($"{RootConfigName}:Middleware:Response"));
            services.Configure<ClientUniqueIdGeneratorConfig>(configuration.GetSection($"{RootConfigName}:Generator:ClientUniqueId"));
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
                services.TryAddScoped<IClientUniqueIdGenerator, ClientUniqueIdFromHeaderGenerator>();
                services.TryAddScoped<ITraceIdGenerator, TraceIdFromHeaderGenerator>();
            }
            else
            {
                services.TryAddScoped<IClientUniqueIdGenerator, CookieClientUniqueIdGenerator>();
                services.TryAddScoped<ITraceIdGenerator, TraceIdGenerator>();
            }

            //Factories
            services.TryAddScoped<IServerLogFactory, ServiceLogFactory>();
            services.TryAddScoped<IClientLogFactory, ClientLogFactory>();
            services.TryAddScoped<ITrafficLogFactory, TrafficLogFactory>();
            services.TryAddScoped<IRequestLogFactory, RequestLogFactory>();
            services.TryAddScoped<IResponseLogFactory, ResponseLogFactory>();
            services.TryAddScoped<IDatesTrafficFactory, DatesTrafficFactory>();

            if (useJsonLogWriter)
            {
                services.AddJsonTrafficCometLogWriter(configuration);
            }

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