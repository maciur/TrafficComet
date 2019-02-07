using Microsoft.AspNetCore.Http;
using System;
using TrafficComet.Abstracts;
using TrafficComet.Core.Abstracts.Configurations;

namespace TrafficComet.Core.Generators
{
    public class CookieClientIdGenerator : IClientIdGenerator
    {
        protected IClientIdGeneratorConfiguration Config { get; }
        protected IHttpContextAccessor HttpContextAccessor { get; }

        public CookieClientIdGenerator(IHttpContextAccessor httpContextAccessor,
            IClientIdGeneratorConfiguration clientIdGeneratorConfiguration)
        {
            HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            Config = clientIdGeneratorConfiguration
                ?? throw new ArgumentNullException(nameof(clientIdGeneratorConfiguration));
        }

        public bool TryGenerateClientId(out string clientId)
        {
            clientId = string.Empty;
            if (!HttpContextAccessor.HttpContext.Request.Cookies.TryGetValue(Config.CookieName, out clientId))
            {
                clientId = Guid.NewGuid().ToString();
                HttpContextAccessor.HttpContext.Response.Cookies.Append(Config.CookieName, clientId, new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(1)
                });
            }
            return true;
        }
    }
}