using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using TrafficComet.Core.Configs;

namespace TrafficComet.Core.Extensions
{
    public static class OptionsExtensions
	{
		public static bool IgnoreRequest<TMiddlewareConfig>(this IOptionsSnapshot<TMiddlewareConfig> optionsSnapshot, PathString requestPath)
            where TMiddlewareConfig : BaseMiddlewareConfig, new()
        {
            if (optionsSnapshot?.Value?.StopLogging ?? false)
                return true;

            if (requestPath.HasValue)
            {
                if (requestPath.Value.Contains(".") && !optionsSnapshot.Value.StartLoggingFileRequest)
                    return true;

                if (optionsSnapshot != null && optionsSnapshot != null)
                {
                    if (optionsSnapshot.Value.StopLogging)
                        return true;

                    if (!optionsSnapshot.Value.IgnoreUrls.SafeAny())
                        return false;

                    return optionsSnapshot.Value.IgnoreUrls.Contains(requestPath.Value);
                }
                return false;
            }
            return true;
        }
	}
}