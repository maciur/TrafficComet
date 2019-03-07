using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using TrafficComet.Abstracts;
using TrafficComet.Abstracts.Accessors;
using TrafficComet.Abstracts.Writers;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Core.Configs;

namespace TrafficComet.Core.Middlewares
{
    public class TrafficCometMiddleware : BaseCometMiddleware
	{
        protected ILogger<TrafficCometMiddleware> Logger { get; }
        protected RequestDelegate Next { get; }
        public TrafficCometMiddleware(RequestDelegate next, IOptionsSnapshot<TrafficCometMiddlewareConfig> config)
			: base(config)
		{
			Next = next ?? throw new ArgumentNullException(nameof(next));
		}

		public TrafficCometMiddleware(RequestDelegate next, ILogger<TrafficCometMiddleware> logger, 
            IOptionsSnapshot<TrafficCometMiddlewareConfig> config)
			: this(next, config)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task Invoke(HttpContext httpContext, ITrafficCometMiddlewaresAccessor trafficCometAccessor,
			ITrafficLogWriter logWriter, ITrafficLogFactory logFactory, ITraceIdGenerator traceIdGenerator,
			IClientIdGenerator clientUniqueIdGenerator)
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
			internalTrafficCometAccessor.InitContextValues();
			internalTrafficCometAccessor.ApplicationId = ((TrafficCometMiddlewareConfig)Config.Value).ApplicationId;
			internalTrafficCometAccessor.IgnoreWholeRequest = IgnoreThisRequest(httpContext.Request.Path);

			var clientIdReaded = clientUniqueIdGenerator.TryGenerateClientId(out string clientId);
			var traceIdReaded = traceIdGenerator.TryGenerateTraceId(out string traceId);

			if (!internalTrafficCometAccessor.IgnoreWholeRequest && clientIdReaded && traceIdReaded)
			{
                BeforeExecuteNextMiddleware(ref internalTrafficCometAccessor, clientId, traceId);
				await Next(httpContext);

				try
				{
					AfterExecutedNextMiddleware(ref internalTrafficCometAccessor);
					var ignoreRequest = internalTrafficCometAccessor.IgnoreRequest;
					var ignoreResponse = internalTrafficCometAccessor.IgnoreResponse;

					var log = logFactory.Create();
					if (log != null)
					{
                        _ = logWriter.SaveLog(log);
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
            string clientid, string traceId)
		{
            trafficCometAccessor.ClientId = clientid;
            trafficCometAccessor.TraceId = traceId;
            trafficCometAccessor.StartDateUtc = DateTime.UtcNow;
            trafficCometAccessor.StartDateLocal = DateTime.Now.ToLocalTime();
		}
	}
}