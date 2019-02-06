using NUnit.Framework;
using System.Linq;
using TrafficComet.Core.Tests.Mock;
using TrafficComet.Core.Tests.Tests.Mocks;

namespace TrafficComet.Core.Tests.Tests.Factories
{
	[TestFixture(Category = "ResponseLogFactory Tests")]
	public class ResponseLogFactory_Tests
	{
		[Test]
		public void CreateCorrectResponseLog()
		{
			var httpContextAccessor = MockServicesHelper.CreateHttpContextAccessor();
			var responseLogFactory = MockServicesHelper.CreateResponseLogFactory(httpContextAccessor: httpContextAccessor);

			var responseLog = responseLogFactory.Create(MockStaticData.RequestBody);

			Assert.NotNull(responseLog);
			Assert.AreEqual(responseLog.Body, MockStaticData.RequestBody);
			Assert.AreEqual(responseLog.Status, 200);
			Assert.AreEqual(responseLog.Headers, MockStaticData.ResponseHeaders.ToDictionary(x => x.Key, y => y.Value));
			Assert.AreEqual(responseLog.CustomParams, MockStaticData.ResponseCustomParams);
		}

		[Test]
		public void IgnoreHeaders()
		{
			var httpContextAccessor = MockServicesHelper.CreateHttpContextAccessor(MockStaticData.HttpMethodGet);
			var responseLogFactory = MockServicesHelper.CreateResponseLogFactory(httpContextAccessor: httpContextAccessor,
				responseLogFactoryConfig: MockStaticData.ResponseLogFactoryConfig_Ignore);

			var requestLog = responseLogFactory.Create(MockStaticData.RequestBody);
			Assert.NotNull(requestLog);
			Assert.False(requestLog.Headers.ContainsKey(MockStaticData.ResponseHeaderToIgnore));
		}
	}
}