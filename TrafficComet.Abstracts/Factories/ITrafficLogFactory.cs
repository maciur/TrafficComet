using TrafficComet.Abstracts.Logs;

namespace TrafficComet.Abstractss.Factories
{
    public interface ITrafficLogFactory
	{
        ITrafficLog Create();
	}
}