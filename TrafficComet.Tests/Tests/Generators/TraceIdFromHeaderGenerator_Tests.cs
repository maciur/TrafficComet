using NUnit.Framework;
using System;
using TrafficComet.Core.Generators;
using TrafficComet.Core.Tests.Mock;
using TrafficComet.Core.Tests.Tests.Mocks;

namespace TrafficComet.Core.Tests.Tests.Generators
{
	[TestFixture(Category = "TraceIdFromHeaderGenerator Tests")]
	public class TraceIdFromHeaderGenerator_Tests
	{
		[Test]
		public void Correct_GetTraceIdFromHeader()
		{
			var httpContextAccessor = MockServicesHelper.CreateHttpContextAccessor(requestHeaders: MockStaticData.RequestHeadersWithTrackIds);
			var traceIdGenerator = new TraceIdFromHeaderGenerator(httpContextAccessor);
			Assert.AreEqual(traceIdGenerator.GenerateTraceId(), MockStaticData.TraceId);
		}

		[Test]
		public void ThrowException_GetTraceIdFromHeader()
		{
			var traceIdGenerator = new TraceIdFromHeaderGenerator(MockServicesHelper.CreateHttpContextAccessor());
			Assert.Throws<NullReferenceException>(() => traceIdGenerator.GenerateTraceId());
		}
	}
}