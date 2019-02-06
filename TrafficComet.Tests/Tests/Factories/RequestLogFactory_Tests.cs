using NUnit.Framework;
using System.Linq;
using TrafficComet.Core.Tests.Mock;
using TrafficComet.Core.Tests.Tests.Mocks;

namespace TrafficComet.Core.Tests.Tests.Factories
{
	[TestFixture(Category = "RequestLogFactory Tests")]
	public class RequestLogFactory_Tests
	{
		[Test]
		public void CreateCorrectRequestLog()
		{
			var httpContextAccessor = MockServicesHelper.CreateHttpContextAccessor(MockStaticData.HttpMethodGet);
			var requestLogFactory = MockServicesHelper.CreateRequestLogFactory(httpContextAccessor: httpContextAccessor);

			var requestLog = requestLogFactory.Create(MockStaticData.RequestBody);

			Assert.NotNull(requestLog);
			Assert.AreEqual(requestLog.Body, MockStaticData.RequestBody);
			Assert.AreEqual(requestLog.Headers, MockStaticData.RequestHeaders.ToDictionary(x => x.Key, y => y.Value));
			Assert.AreEqual(requestLog.Cookies, MockStaticData.RequestCookies.ToDictionary(x=>x.Key, y=>y.Value));
			Assert.AreEqual(requestLog.HttpMethod, MockStaticData.HttpMethodGet);
			Assert.AreEqual(requestLog.Path, MockStaticData.RequestPath);
			Assert.AreEqual(requestLog.CustomParams, MockStaticData.RequestCustomParams);
		}

		[Test]
		public void IgnoreHeaders()
		{
			var httpContextAccessor = MockServicesHelper.CreateHttpContextAccessor(MockStaticData.HttpMethodGet);
			var requestLogFactory = MockServicesHelper.CreateRequestLogFactory(httpContextAccessor: httpContextAccessor,
				requestLogFactoryConfig: MockStaticData.RequestLogFactoryConfig_Ignore);

			var requestLog = requestLogFactory.Create(MockStaticData.RequestBody);
			Assert.NotNull(requestLog);
			Assert.False(requestLog.Headers.ContainsKey(MockStaticData.RequestHeaderToIgnore));
		}

		[Test]
		public void IgnoreCookies()
		{
			var httpContextAccessor = MockServicesHelper.CreateHttpContextAccessor(MockStaticData.HttpMethodGet);
			var requestLogFactory = MockServicesHelper.CreateRequestLogFactory(httpContextAccessor: httpContextAccessor,
				requestLogFactoryConfig: MockStaticData.RequestLogFactoryConfig_Ignore);

			var requestLog = requestLogFactory.Create(MockStaticData.RequestBody);
			Assert.NotNull(requestLog);
			Assert.False(requestLog.Cookies.ContainsKey(MockStaticData.RequestCookieToIgnore));
		}
	}
}