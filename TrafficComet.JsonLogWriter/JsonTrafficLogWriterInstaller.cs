using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrafficComet.Abstracts.Writers;
using TrafficComet.Core.Consts;

namespace TrafficComet.JsonLogWriter
{
    public static class JsonTrafficLogWriterInstaller
	{
		public static IServiceCollection AddJsonTrafficCometLogWriter(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<JsonTrafficLogWriterConfig>(configuration.GetSection($"{TrafficCometConstValues.RootConfigName}:TrafficLogWriter"));
			services.AddScoped<ITrafficLogWriter, JsonTrafficLogWriter>();
			return services;
		}
	}
}