using TrafficComet.Abstracts.Logs;

namespace TrafficComet.Abstracts.Factories
{
    public interface ITrafficLogFactory
	{
        ITrafficLog Create();
	}
}