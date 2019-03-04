using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using TrafficComet.Core.Configs;
using TrafficComet.Core.Extensions;

namespace TrafficComet.Core.Middlewares
{
	public class BaseCometMiddleware
	{
		protected IOptions<BaseMiddlewareConfig> Config { get; }

		public BaseCometMiddleware(IOptions<BaseMiddlewareConfig> config)
		{
			Config = config;
		}

		protected const string ContentEncodingGzip = "gzip";
		private const string ContentEncodingDeflate = "deflate";

		protected internal bool IgnoreThisRequest(PathString requestPath)
		{
            if (Config.Value.StopLogging)
                return true;

			if (requestPath.HasValue)
			{
				if (requestPath.Value.Contains(".") && !Config.Value.StartLoggingFileRequest)
					return true;

				if (Config != null && Config.Value != null)
				{
					if (Config.Value.StopLogging)
						return true;

					if (!Config.Value.IgnoreUrls.SafeAny())
						return false;

					return Config.Value.IgnoreUrls.Contains(requestPath.Value);
				}
				return false;
			}
			return true;
		}
	}
}