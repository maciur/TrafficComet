using Microsoft.AspNetCore.Http;
using TrafficComet.Abstracts;
using TrafficComet.Core.Consts;

namespace TrafficComet.Core.Generators
{
	public class ClientUniqueIdFromHeaderGenerator : BaseGeneratorFromHeader, IClientUniqueIdGenerator
	{
		public ClientUniqueIdFromHeaderGenerator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public string GenerateClientUniqueId()
		{
			return GetHeaderValue(TrafficCometConstValues.ClientUniqueIdHeader);
		}
	}
}