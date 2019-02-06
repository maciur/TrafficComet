using Microsoft.AspNetCore.Http;
using TrafficComet.Abstracts;
using TrafficComet.Core.Consts;

namespace TrafficComet.Core.Generators
{
	public class TraceIdFromHeaderGenerator : BaseGeneratorFromHeader, ITraceIdGenerator
	{
		public TraceIdFromHeaderGenerator(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public string GenerateTraceId()
		{
			return GetHeaderValue(TrafficCometConstValues.TraceIdHeader);
		}
	}
}