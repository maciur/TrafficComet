using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using TrafficComet.Abstracts.Accessors;
using TrafficComet.Abstracts.Readers;
using TrafficComet.Core.Configs;
using TrafficComet.Core.Extensions;

namespace TrafficComet.Core.Middlewares
{
	public class ResponseReadMiddleware : BaseCometMiddleware
	{
		protected RequestDelegate Next { get; }
		protected ILogger<RequestReadMiddleware> Logger { get; }

		public ResponseReadMiddleware(RequestDelegate next, IOptions<ResponseReadMiddlewareConfig> config) : base(config)
		{
			Next = next ?? throw new ArgumentNullException(nameof(next));
		}

		public ResponseReadMiddleware(ILogger<RequestReadMiddleware> logger, RequestDelegate next,
			IOptions<ResponseReadMiddlewareConfig> config) : this(next, config)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task Invoke(HttpContext httpContext, ITrafficCometMiddlewaresAccessor trafficCometAccessor,
			IDynamicBodyReader dynamicBodyReader)
		{
			if (trafficCometAccessor == null)
				throw new ArgumentNullException(nameof(trafficCometAccessor));

			if (dynamicBodyReader == null)
				throw new ArgumentNullException(nameof(dynamicBodyReader));

			TrafficCometMiddlewaresAccessor internalTrafficCometAccessor = (TrafficCometMiddlewaresAccessor)trafficCometAccessor;
			internalTrafficCometAccessor.IgnoreResponse = !internalTrafficCometAccessor.IgnoreWholeRequest && IgnoreThisRequest(httpContext.Request.Path);

			if (!internalTrafficCometAccessor.IgnoreResponse)
			{
				Stream originalResponseBody = httpContext.Response.Body;
				try
				{
					using (var buffer = new MemoryStream())
					{
						httpContext.Response.Body = buffer;
						await Next(httpContext);
						buffer.Position = 0L;
						await buffer.CopyToAsync(originalResponseBody);

						var bodyCompressed = httpContext.ResponseBodyIsCompressed(out string compressionType);
						internalTrafficCometAccessor.ResponseBody = dynamicBodyReader
							.ReadBody(buffer, bodyCompressed, compressionType);
					}
				}
				catch (Exception ex)
				{
					if (Logger != null && Logger.IsEnabled(LogLevel.Error))
					{
						Logger.LogError(new EventId(91, $"TrafficComet.{nameof(ResponseReadMiddleware)}"), ex, ex.ToString());
					}
				}
				finally
				{
					httpContext.Response.Body = originalResponseBody;
				}
			}
			else
			{
				await Next(httpContext);
			}
		}
	}
}