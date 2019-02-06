using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using TrafficComet.Abstracts;
using TrafficComet.Abstracts.Accessors;
using TrafficComet.Abstracts.Writers;
using TrafficComet.Abstractss.Factories;
using TrafficComet.Core.Configs;

namespace TrafficComet.Core.Middlewares
{
    public class TrafficCometMiddleware : BaseCometMiddleware
	{
        protected ILogger<TrafficCometMiddleware> Logger { get; }
        protected RequestDelegate Next { get; }
        public TrafficCometMiddleware(RequestDelegate next, IOptions<TrafficCometMiddlewareConfig> config)
			: base(config)
		{
			Next = next ?? throw new ArgumentNullException(nameof(next));
		}

		public TrafficCometMiddleware(RequestDelegate next, ILogger<TrafficCometMiddleware> logger, IOptions<TrafficCometMiddlewareConfig> config)
			: this(next, config)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task Invoke(HttpContext httpContext, ITrafficCometMiddlewaresAccessor trafficCometAccessor,
			ITrafficLogWriter logWriter, ITrafficLogFactory logFactory, ITraceIdGenerator traceIdGenerator,
			IClientUniqueIdGenerator clientUniqueIdGenerator)
		{
			if (trafficCometAccessor == null)
				throw new ArgumentNullException(nameof(trafficCometAccessor));

			if (logWriter == null)
				throw new ArgumentNullException(nameof(logWriter));

			if (logFactory == null)
				throw new ArgumentNullException(nameof(logFactory));

			if (traceIdGenerator == null)
				throw new ArgumentNullException(nameof(traceIdGenerator));

			if (clientUniqueIdGenerator == null)
				throw new ArgumentNullException(nameof(clientUniqueIdGenerator));

			TrafficCometMiddlewaresAccessor internalTrafficCometAccessor = (TrafficCometMiddlewaresAccessor)trafficCometAccessor;
			internalTrafficCometAccessor.ApplicationId = ((TrafficCometMiddlewareConfig)Config.Value).ApplicationId;
			internalTrafficCometAccessor.IgnoreWholeRequest = IgnoreThisRequest(httpContext.Request.Path);

			if (!internalTrafficCometAccessor.IgnoreWholeRequest)
			{
				BeforeExecuteNextMiddleware(ref internalTrafficCometAccessor, ref traceIdGenerator, ref clientUniqueIdGenerator);
				await Next(httpContext);

				try
				{
					AfterExecutedNextMiddleware(ref internalTrafficCometAccessor);
					var ignoreRequest = internalTrafficCometAccessor.IgnoreRequest;
					var ignoreResponse = internalTrafficCometAccessor.IgnoreResponse;

					var log = logFactory.Create();
					if (log != null)
					{
						var writerResult = logWriter.SaveLog(log);
                    }
				}
				catch (Exception ex)
				{
					if (Logger != null && Logger.IsEnabled(LogLevel.Error))
					{
						Logger.LogError(new EventId(92, $"TrafficComet.{nameof(TrafficCometMiddleware)}"), ex, ex.ToString());
					}
				}
			}
			else
			{
				await Next(httpContext);
			}
		}

        protected internal void AfterExecutedNextMiddleware(ref TrafficCometMiddlewaresAccessor trafficCometAccessor)
        {
            trafficCometAccessor.EndDateUtc = DateTime.UtcNow;
            trafficCometAccessor.EndDateLocal = DateTime.Now.ToLocalTime();
        }

        protected internal void BeforeExecuteNextMiddleware(ref TrafficCometMiddlewaresAccessor trafficCometAccessor,
                    ref ITraceIdGenerator traceIdGenerator, ref IClientUniqueIdGenerator clientUniqueIdGenerator)
		{
            trafficCometAccessor.TraceId = traceIdGenerator.GenerateTraceId();
            trafficCometAccessor.ClientUniqueId = clientUniqueIdGenerator.GenerateClientUniqueId();
            trafficCometAccessor.StartDateUtc = DateTime.UtcNow;
            trafficCometAccessor.StartDateLocal = DateTime.Now.ToLocalTime();
		}
	}
}