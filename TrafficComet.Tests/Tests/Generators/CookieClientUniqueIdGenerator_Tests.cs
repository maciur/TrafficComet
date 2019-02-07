using NUnit.Framework;
using System;
using TrafficComet.Core.Generators;
using TrafficComet.Core.Tests.Mock;
using TrafficComet.Core.Tests.Tests.Mocks;

namespace TrafficComet.Core.Tests.Tests.Generators
{
	[TestFixture(Category = "CookieClientUniqueIdGenerator Tests")]
	public class CookieClientUniqueIdGenerator_Tests
	{
		[Test]
		public void GetClientUniqueId_CookieExists()
		{
			var httpContextAccessor = MockServicesHelper
				.CreateHttpContextAccessor(requestCookie: MockStaticData.RequestCookiesWithTrackIds);
			var generator = new CookieClientIdGenerator(httpContextAccessor, null);
			Assert.AreEqual(generator.GenerateClientId(), MockStaticData.UserUniqueId);
		}

		[Test]
		public void GetClientUniqueId_CookieDoesntExist()
		{
			var generator = new CookieClientIdGenerator(MockServicesHelper
				.CreateHttpContextAccessor(), null);

			Assert.True(Guid.TryParse(generator.GenerateClientId(), out Guid clientUniqueId));
		}
	}
}