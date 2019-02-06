﻿using NUnit.Framework;
using System;
using TrafficComet.Core.Generators;
using TrafficComet.Core.Tests.Mock;
using TrafficComet.Core.Tests.Tests.Mocks;

namespace TrafficComet.Core.Tests.Tests.Generators
{
	[TestFixture(Category = "ClientUniqueIdFromHeaderGenerator Tests")]
	public class ClientUniqueIdFromHeaderGenerator_Tests
	{
		[Test]
		public void Correct_GetClientUniqueIdFromHeader()
		{
			var httpContextAccessor = MockServicesHelper.CreateHttpContextAccessor(requestHeaders: MockStaticData.RequestHeadersWithTrackIds);
			var traceIdGenerator = new ClientUniqueIdFromHeaderGenerator(httpContextAccessor);
			Assert.AreEqual(traceIdGenerator.GenerateClientUniqueId(), MockStaticData.UserUniqueId);
		}

		[Test]
		public void ThrowException_GetClientUniqueIdFromHeader()
		{
			var traceIdGenerator = new ClientUniqueIdFromHeaderGenerator(MockServicesHelper.CreateHttpContextAccessor());
			Assert.Throws<NullReferenceException>(() => traceIdGenerator.GenerateClientUniqueId());
		}
	}
}