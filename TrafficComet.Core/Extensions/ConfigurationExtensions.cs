using Microsoft.Extensions.Configuration;
using System;
using TrafficComet.Abstracts.Consts;

namespace TrafficComet.Core.Extensions
{
    internal static class ConfigurationExtensions
    {
        internal static IConfiguration GetSection(this IConfiguration configuration, params string[] pathSemgments)
        {
            if (!pathSemgments.SafeAny())
                throw new ArgumentNullException(nameof(pathSemgments));

            return configuration.GetSection(string.Join(':', pathSemgments));
        }

        internal static bool StopLogging(this IConfiguration configuration)
        {
            if (configuration == null)
                throw new NullReferenceException(nameof(configuration));

            var stopLogging = configuration.GetValue<bool>(string.Join(':', ConfigurationSelectors.ROOT,
                ConfigurationSelectors.MIDDLEWARE, ConfigurationSelectors.MIDDLEWARE_ROOT,
                ConfigurationSelectors.MIDDLEWARE_STOP_LOGGING));

            return stopLogging;
        }
    }
}