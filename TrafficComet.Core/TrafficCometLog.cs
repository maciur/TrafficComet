using System.Collections.Generic;
using TrafficComet.Abstracts.Logs;
using TrafficComet.Abstracts.Logs.Client;
using TrafficComet.Abstracts.Logs.Request;
using TrafficComet.Abstracts.Logs.Response;

namespace TrafficComet.Core
{
    public class TrafficCometLog : ITrafficLog
    {
        public string TraceId { get; set; }
        public string ApplicationId { get; set; }
        public IServerTrafficLog Server { get; set; }
        public IDatesTrafficLog Dates { get; set; }
        public IClientTrafficLog Client { get; set; }
        public IRequestLog Request { get; set; }
        public IResponseLog Response { get; set; }
        public IDictionary<string, string> CustomParams { get; set; }
    }
}