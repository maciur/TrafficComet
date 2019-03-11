using Microsoft.Extensions.Options;
using TrafficComet.Core.Abstracts.Configurations;
using TrafficComet.Core.Consts;

namespace TrafficComet.Core.Configs
{
    public class ClientIdGeneratorConfiguration : IClientIdGeneratorConfiguration
    {
        protected IOptionsMonitor<ClientIdGeneratorConfig> Config { get; }

        public string CookieName => !string.IsNullOrEmpty(RawConfig?.CookieName) ?
            RawConfig.CookieName : DefaultTrafficCometValues.CLIENT_ID_COOKIE_NAME;

        public string HeaderName => !string.IsNullOrEmpty(RawConfig?.HeaderName) ?
            RawConfig.HeaderName : DefaultTrafficCometValues.CLIENT_ID_HEADER;

        public ClientIdGeneratorConfig RawConfig => Config?.CurrentValue;

        public ClientIdGeneratorConfiguration(IOptionsMonitor<ClientIdGeneratorConfig> clientIdGeneratorConfig)
        {
            Config = clientIdGeneratorConfig;
        }
    }
}