namespace TrafficComet.Abstracts.Logs
{
    public interface IServerTrafficLog
    {
        string IpAddress { get; set; }
        string MachineName { get; set; }
    }
}