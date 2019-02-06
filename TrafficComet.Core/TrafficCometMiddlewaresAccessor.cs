using System;
using System.Collections.Generic;
using TrafficComet.Abstracts.Accessors;

namespace TrafficComet.Core
{
    public class TrafficCometMiddlewaresAccessor : ITrafficCometMiddlewaresAccessor
    {
        public TrafficCometMiddlewaresAccessor()
        {
            CustomParams = new Dictionary<string, string>();
            RequestCustomParams = new Dictionary<string, string>();
            ResponseCustomParams = new Dictionary<string, string>();
        }

        public dynamic RequestBody { get; internal set; }
        public dynamic ResponseBody { get; internal set; }
        public DateTime StartDateUtc { get; internal set; }
        public DateTime EndDateUtc { get; internal set; }
        public DateTime StartDateLocal { get; internal set; }
        public DateTime EndDateLocal { get; internal set; }
        public string TraceId { get; internal set; }
        public string ClientUniqueId { get; internal set; }
        public string ApplicationId { get; internal set; }
        public bool IgnoreWholeRequest { get; internal set; }
        public bool IgnoreRequest { get; internal set; }
        public bool IgnoreResponse { get; internal set; }

        public IDictionary<string, string> CustomParams { get; set; }
        public IDictionary<string, string> RequestCustomParams { get; set; }
        public IDictionary<string, string> ResponseCustomParams { get; set; }
    }
}