using TrafficComet.Abstracts.Logs;

namespace TrafficComet.Abstracts.Factories
{
    public interface IServerLogFactory
    {
        IServerTrafficLog Create();
    }
}