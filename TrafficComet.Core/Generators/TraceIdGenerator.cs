using System;
using TrafficComet.Abstracts;

namespace TrafficComet.Core.Generators
{
    public class TraceIdGenerator : ITraceIdGenerator
    {
        public bool TryGenerateTraceId(out string clientId)
        {
            clientId = Guid.NewGuid().ToString();
            return true;
        }
    }
}