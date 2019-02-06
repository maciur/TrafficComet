using System.Collections.Generic;
using TrafficComet.Abstracts.Logs.Response;

namespace TrafficComet.Core
{
    public class ResponseLog : IResponseLog
    {
        public IDictionary<string, string> Headers { get; set; }
        public IDictionary<string, string> CustomParams { get; set; }
        public dynamic Body { get; set; }
        public int Status { get; set; }
    }
}