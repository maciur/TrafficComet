using TrafficComet.Abstracts.Logs;

namespace TrafficComet.Core
{
    public class ServerTrafficLog : IServerTrafficLog
    {
        public string IpAddress { get; set; }
        public string MachineName { get; set; }
    }
}
