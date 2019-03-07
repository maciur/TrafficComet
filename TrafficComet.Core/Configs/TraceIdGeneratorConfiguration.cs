﻿using Microsoft.Extensions.Options;
using TrafficComet.Core.Abstracts.Configurations;
using TrafficComet.Core.Consts;

namespace TrafficComet.Core.Configs
{
    public class TraceIdGeneratorConfiguration : ITraceIdGeneratorConfiguration
    {
        protected IOptionsSnapshot<TraceIdGeneratorConfig> Config { get; }

        public string HeaderName => !string.IsNullOrEmpty(Config?.Value?.HeaderName) ?
            Config.Value.HeaderName : DefaultTrafficCometValues.TRACE_ID_HEADER;

        public TraceIdGeneratorConfig RawConfig => Config?.Value;

        public TraceIdGeneratorConfiguration(IOptionsSnapshot<TraceIdGeneratorConfig> clientIdGeneratorConfig)
        {
            Config = clientIdGeneratorConfig;
        }
    }
}