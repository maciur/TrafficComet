using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
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
    public class RequestReadMiddleware : BaseCometMiddleware
    {
        protected ILogger<RequestReadMiddleware> Logger { get; }
        protected RequestDelegate Next { get; }

        public RequestReadMiddleware(RequestDelegate next,
            IOptionsSnapshot<RequestReadMiddlewareConfig> config) : base(config)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public RequestReadMiddleware(ILogger<RequestReadMiddleware> logger, RequestDelegate next,
            IOptionsSnapshot<RequestReadMiddlewareConfig> config)
            : this(next, config)
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
            internalTrafficCometAccessor.IgnoreRequest = !internalTrafficCometAccessor.IgnoreWholeRequest
                && IgnoreThisRequest(httpContext.Request.Path);

            if (!internalTrafficCometAccessor.IgnoreRequest)
            {
                MemoryStream originalRequestBody = new MemoryStream();
                await httpContext.Request.Body.CopyToAsync(originalRequestBody);
                originalRequestBody.Position = 0L;

                try
                {
                    httpContext.Request.EnableRewind();
                    using (var buffer = new MemoryStream())
                    {
                        await originalRequestBody.CopyToAsync(buffer);
                        originalRequestBody.Position = 0L;

                        var bodyCompressed = httpContext.RequestBodyIsCompressed(out string compressionType);
                        internalTrafficCometAccessor.RequestBody = dynamicBodyReader
                            .ReadBody(buffer, bodyCompressed, compressionType);
                    }
                }
                catch (Exception ex)
                {
                    if (Logger != null && Logger.IsEnabled(LogLevel.Error))
                    {
                        Logger.LogError(new EventId(90, $"TrafficComet.{nameof(RequestReadMiddleware)}"), ex, ex.ToString());
                    }
                }
                finally
                {
                    httpContext.Request.Body = originalRequestBody;
                }
                await Next(httpContext);
            }
            else
            {
                await Next(httpContext);
            }
        }
    }
}