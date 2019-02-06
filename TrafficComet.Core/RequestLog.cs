using System.Collections.Generic;
using TrafficComet.Abstracts.Logs.Request;

namespace TrafficComet.Core
{
    public class RequestLog : IRequestLog
    {
        public dynamic Body { get; set; }
        public IDictionary<string, string> Cookies { get; set; }
        public IDictionary<string, string> CustomParams { get; set; }
        public string FullUrl { get; set; }
        public IDictionary<string, string> Headers { get; set; }
        public string HttpMethod { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> QueryParams { get; set; }
    }
}