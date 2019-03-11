using Microsoft.Extensions.Options;
using TrafficComet.Core.Abstracts.Configurations;
using TrafficComet.Core.Consts;

namespace TrafficComet.Core.Configs
{
    public class TraceIdGeneratorConfiguration : ITraceIdGeneratorConfiguration
    {
        protected IOptionsMonitor<TraceIdGeneratorConfig> Config { get; }

        public string HeaderName => !string.IsNullOrEmpty(RawConfig?.HeaderName) ?
            RawConfig.HeaderName : DefaultTrafficCometValues.TRACE_ID_HEADER;

        public TraceIdGeneratorConfig RawConfig => Config?.CurrentValue;

        public TraceIdGeneratorConfiguration(IOptionsMonitor<TraceIdGeneratorConfig> clientIdGeneratorConfig)
        {
            Config = clientIdGeneratorConfig;
        }
    }
}