using System;
using TrafficComet.Abstracts;

namespace TrafficComet.Core.Generators
{
	public class TraceIdGenerator : ITraceIdGenerator
	{
		public string GenerateTraceId()
		{
			return Guid.NewGuid().ToString();
		}
	}
}