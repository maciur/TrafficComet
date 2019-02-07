using TrafficComet.Core.Configs;

namespace TrafficComet.Core.Abstracts.Configurations
{
    public interface IClientIdGeneratorConfiguration
    {
        string CookieName { get; }
        string HeaderName { get; }
        ClientIdGeneratorConfig RawConfig { get; }
    }
}
