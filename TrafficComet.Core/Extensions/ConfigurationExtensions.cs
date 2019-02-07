using Microsoft.Extensions.Configuration;
using System;

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
    }
}