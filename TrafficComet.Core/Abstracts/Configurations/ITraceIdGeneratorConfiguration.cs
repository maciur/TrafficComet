using TrafficComet.Core.Configs;

namespace TrafficComet.Core.Abstracts.Configurations
{
    public interface ITraceIdGeneratorConfiguration
    {
        string HeaderName { get; }
        TraceIdGeneratorConfig RawConfig { get; }
    }
}
