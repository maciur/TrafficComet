using Microsoft.Extensions.Options;
using TrafficComet.Core.Abstracts.Configurations;
using TrafficComet.Core.Consts;

namespace TrafficComet.Core.Configs
{
    public class ClientIdGeneratorConfiguration : IClientIdGeneratorConfiguration
    {
        protected IOptionsSnapshot<ClientIdGeneratorConfig> Config { get; }

        public string CookieName => !string.IsNullOrEmpty(Config?.Value?.CookieName) ?
            Config.Value.CookieName : DefaultTrafficCometValues.CLIENT_ID_COOKIE_NAME;

        public string HeaderName => !string.IsNullOrEmpty(Config?.Value?.HeaderName) ?
            Config.Value.HeaderName : DefaultTrafficCometValues.CLIENT_ID_HEADER;

        public ClientIdGeneratorConfig RawConfig => Config?.Value;

        public ClientIdGeneratorConfiguration(IOptionsSnapshot<ClientIdGeneratorConfig> clientIdGeneratorConfig)
        {
            Config = clientIdGeneratorConfig;
        }
    }
}