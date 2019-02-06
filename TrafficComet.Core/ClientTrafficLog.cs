using TrafficComet.Abstracts.Logs.Client;

namespace TrafficComet.Core
{
    public class ClientTrafficLog : IClientTrafficLog
    {
        public string Id { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
    }
}