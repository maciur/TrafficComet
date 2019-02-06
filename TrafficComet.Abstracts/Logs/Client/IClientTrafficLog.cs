namespace TrafficComet.Abstracts.Logs.Client
{
    public interface IClientTrafficLog
    {
        string Id { get; set; }
        string IpAddress { get; set; }
        string UserAgent { get; set; }
    }
}