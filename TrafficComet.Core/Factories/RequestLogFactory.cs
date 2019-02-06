using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using TrafficComet.Abstracts.Accessors;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Abstracts.Logs.Request;
using TrafficComet.Core.Configs;
using TrafficComet.Core.Extensions;

namespace TrafficComet.Core.Factories
{
    public class RequestLogFactory : IRequestLogFactory
    {
        protected HttpRequest Request => HttpContextAccessor.HttpContext.Request;
        protected IOptions<RequestLogFactoryConfig> Config { get; }
        protected IHttpContextAccessor HttpContextAccessor { get; }
        protected ITrafficCometMiddlewaresAccessor TrafficCometAccessor { get; }

        public RequestLogFactory(IHttpContextAccessor httpContextAccessor, IOptions<RequestLogFactoryConfig> config,
            ITrafficCometMiddlewaresAccessor trafficCometAccessor)
        {
            HttpContextAccessor = httpContextAccessor 
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            TrafficCometAccessor = trafficCometAccessor 
                ?? throw new ArgumentNullException(nameof(trafficCometAccessor));

            Config = config;
        }

        public IRequestLog Create(dynamic requestBody)
        {
            return new RequestLog
            {
                Body = requestBody,
                Headers = Request.GetHeaders(Config.Value?.IgnoreHeaders),
                Cookies = Request.GetCookies(Config.Value?.IgnoreCookies),
                HttpMethod = Request.Method,
                Path = Request.Path.Value,
                FullUrl = Request.GetFullUrl(),
                QueryParams = Request.GetQueryParams(),
                CustomParams = TrafficCometAccessor.RequestCustomParams
            };
        }
    }
}