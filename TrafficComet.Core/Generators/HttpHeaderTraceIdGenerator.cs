using Microsoft.AspNetCore.Http;
using System;
using TrafficComet.Abstracts;
using TrafficComet.Core.Abstracts.Configurations;

namespace TrafficComet.Core.Generators
{
    public class HttpHeaderTraceIdGenerator : BaseGeneratorFromHeader, ITraceIdGenerator
    {
        protected ITraceIdGeneratorConfiguration Config { get; }

        public HttpHeaderTraceIdGenerator(IHttpContextAccessor httpContextAccessor,
            ITraceIdGeneratorConfiguration traceIdGeneratorConfiguration) : base(httpContextAccessor)
        {
            Config = traceIdGeneratorConfiguration
                ?? throw new ArgumentNullException(nameof(traceIdGeneratorConfiguration));
        }

        public bool TryGenerateTraceId(out string traceId)
        {
			return TryGetHeaderValue(Config.HeaderName, out traceId);
        }
    }
}