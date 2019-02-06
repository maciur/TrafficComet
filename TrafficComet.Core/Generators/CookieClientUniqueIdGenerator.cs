using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using TrafficComet.Abstracts;
using TrafficComet.Core.Configs;
using TrafficComet.Core.Consts;
using TrafficComet.Core.Extensions;

namespace TrafficComet.Core.Generators
{
	public class CookieClientUniqueIdGenerator : IClientUniqueIdGenerator
	{
		protected IHttpContextAccessor HttpContextAccessor { get; }
		protected IOptions<ClientUniqueIdGeneratorConfig> Config { get; }

		public CookieClientUniqueIdGenerator(IHttpContextAccessor httpContextAccessor, IOptions<ClientUniqueIdGeneratorConfig> config)
		{
			HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
			Config = config;
		}

		public string GenerateClientUniqueId()
		{
			string clientId = string.Empty;
			var cookieName = GetCookieName();
			if (!HttpContextAccessor.HttpContext.Request.Cookies.TryGetValue(cookieName, out clientId))
			{
				clientId = Guid.NewGuid().ToString();
				HttpContextAccessor.HttpContext.Response.Cookies.Append(cookieName, clientId, new CookieOptions
				{
					Expires = DateTime.Now.AddYears(1)
				});
			}

			return clientId;
		}

		protected internal string GetCookieName()
		{
			if (Config.IsValid())
			{
				return !string.IsNullOrEmpty(Config.Value.CookieName) ? Config.Value.CookieName : TrafficCometConstValues.DefaultClientUniqueIdCookieName;
			}
			return TrafficCometConstValues.DefaultClientUniqueIdCookieName;
		}
	}
}