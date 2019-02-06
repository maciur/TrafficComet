using TrafficComet.Abstracts.Logs;

namespace TrafficComet.Abstracts.Factories
{
    public interface IDatesTrafficFactory
    {
        IDatesTrafficLog Create();
    }
}