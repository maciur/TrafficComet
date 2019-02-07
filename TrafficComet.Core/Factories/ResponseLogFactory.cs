using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using TrafficComet.Abstracts.Accessors;
using TrafficComet.Abstracts.Logs.Response;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Core.Configs;
using TrafficComet.Core.Extensions;

namespace TrafficComet.Core.Factories
{
    public class ResponseLogFactory : IResponseLogFactory
    {
        protected IHttpContextAccessor HttpContextAccessor { get; }
        protected HttpResponse Response => HttpContextAccessor.HttpContext.Response;
        protected IOptions<ResponseLogFactoryConfig> Config { get; }
        protected ITrafficCometMiddlewaresAccessor TrafficCometAccessor { get; }

        public ResponseLogFactory(IHttpContextAccessor httpContextAccessor, IOptions<ResponseLogFactoryConfig> config,
            ITrafficCometMiddlewaresAccessor trafficCometAccessor)
        {
            HttpContextAccessor = httpContextAccessor
                ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            TrafficCometAccessor = trafficCometAccessor
                ?? throw new ArgumentNullException(nameof(trafficCometAccessor));

            Config = config;
        }

        public IResponseLog Create(dynamic requestBody)
        {
            return new ResponseLog
            {
                Body = requestBody,
                Headers = Response.GetHeaders(),
                Status = Response.StatusCode,
                CustomParams = TrafficCometAccessor.ResponseCustomParams
            };
        }
    }
}