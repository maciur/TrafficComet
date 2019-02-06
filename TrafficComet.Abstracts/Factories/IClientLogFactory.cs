using TrafficComet.Abstracts.Logs.Client;

namespace TrafficComet.Abstracts.Factories
{
    public interface IClientLogFactory
    {
        IClientTrafficLog Create();
    }
}