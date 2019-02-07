using Microsoft.AspNetCore.Http;
using System;
using TrafficComet.Abstracts;
using TrafficComet.Core.Abstracts.Configurations;

namespace TrafficComet.Core.Generators
{
	public class HttpHeaderClientIdGenerator : BaseGeneratorFromHeader, IClientIdGenerator
	{
		protected IClientIdGeneratorConfiguration Config { get; }

		public HttpHeaderClientIdGenerator(IHttpContextAccessor httpContextAccessor,
			IClientIdGeneratorConfiguration clientIdGeneratorConfiguration) : base(httpContextAccessor)
		{
			Config = clientIdGeneratorConfiguration
				?? throw new ArgumentNullException(nameof(clientIdGeneratorConfiguration));
		}

		public bool TryGenerateClientId(out string clientId)
		{
			return TryGetHeaderValue(Config.HeaderName, out clientId);
		}
	}
}