using Moq;
using System;
using TrafficComet.Abstracts;

namespace TrafficComet.Core.Tests.Mock
{
	internal static class ITraceIdGeneratorMockFactory
	{
		internal static ITraceIdGenerator CreateObjectMock(string traceId = null)
		{
			if (string.IsNullOrEmpty(traceId))
				traceId = Guid.NewGuid().ToString();

			var mockObject = new Mock<ITraceIdGenerator>();
			mockObject.Setup(x => x.GenerateTraceId()).Returns(traceId).Verifiable();
			return mockObject.Object;
		}
	}
}