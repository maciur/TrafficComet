using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TrafficComet.Core.Configs;
using TrafficComet.Core.Tests.Mock;
using TrafficComet.Core.Tests.Mocks;
using TrafficComet.Core.Tests.Tests.Mocks;

namespace TrafficComet.Core.Tests.Tests.Middlewares
{
	[TestFixture]
	public class RequestReadMiddleware_Tests
	{
		[Test]
		public async Task RequestBodyReadSuccessfully()
		{
			var accessor = new TrafficCometMiddlewaresAccessor();
			var bodyReader = IStringBodyReaderMockFactory.CreateMockObject(MockStaticData.RequestBody);

			var requestReadMiddleware = RequestReadMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);
			await requestReadMiddleware.Invoke(httpContext, accessor, bodyReader);

			Assert.AreEqual(accessor.RequestBody, MockStaticData.RequestBody);
			Assert.NotNull(httpContext.Request.Body);
			Assert.AreEqual(httpContext.Request.Body.Position, 0L);
		}

		[Test]
		public void ThrowException_NullBodyReader()
		{
			var requestReadMiddleware = RequestReadMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);

			Assert.ThrowsAsync<ArgumentNullException>(() =>
				requestReadMiddleware.Invoke(httpContext, new TrafficCometMiddlewaresAccessor(), null));
		}

		[Test]
		public void ThrowException_NullTrafficCometAccessor()
		{
			var requestReadMiddleware = RequestReadMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);
			var bodyReader = IStringBodyReaderMockFactory.CreateMockObject(MockStaticData.RequestBody);
			Assert.ThrowsAsync<ArgumentNullException>(() =>
				requestReadMiddleware.Invoke(httpContext, null, bodyReader));
		}

		[Test]
		public async Task ThrowException_RollbackOryginalRequestBody()
		{
			var accessor = new TrafficCometMiddlewaresAccessor();
			var requestReadMiddleware = RequestReadMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);
			await requestReadMiddleware.Invoke(httpContext, accessor, IStringBodyReaderMockFactory.CreateMockObjectWithException());

			Assert.IsNull(accessor.RequestBody);
			Assert.NotNull(httpContext.Request.Body);
			Assert.AreEqual(httpContext.Request.Body.Position, 0L);
		}

		[Test]
		public async Task IgnoredRequest()
		{
			var middlewareConfig = new RequestReadMiddlewareConfig
			{
				IgnoreUrls = new[] { MockStaticData.RequestPath }
			};
			await RunMiddlewareWithConfiguration(middlewareConfig);
		}

		[Test]
		public async Task StopLogging()
		{
			var middlewareConfig = new RequestReadMiddlewareConfig
			{
				StopLogging = true
			};
			await RunMiddlewareWithConfiguration(middlewareConfig);
		}

		[Test]
		public async Task StopLoggingFileRequests()
		{
			var accessor = new TrafficCometMiddlewaresAccessor();
			var bodyReader = IStringBodyReaderMockFactory.CreateMockObject(MockStaticData.RequestBody);

			var middlewareConfig = new RequestReadMiddlewareConfig
			{
				StartLoggingFileRequest = false
			};

			var requestReadMiddleware = RequestReadMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext,
				middlewareConfig, "/bootstrap.css");

			await requestReadMiddleware.Invoke(httpContext, accessor, bodyReader);

			Assert.IsNull(accessor.RequestBody);
			Assert.NotNull(httpContext.Request.Body);
			Assert.AreEqual(httpContext.Request.Body.Position, 0L);
		}

		[Ignore("This is only helper method")]
		internal async Task RunMiddlewareWithConfiguration(RequestReadMiddlewareConfig config)
		{
			var accessor = new TrafficCometMiddlewaresAccessor();
			var bodyReader = IStringBodyReaderMockFactory.CreateMockObject(MockStaticData.RequestBody);

			var requestReadMiddleware = RequestReadMiddlewareMockFactory.CreateMockObject(
				out HttpContext httpContext, config);

			await requestReadMiddleware.Invoke(httpContext, accessor, bodyReader);

			Assert.IsNull(accessor.RequestBody);
			Assert.NotNull(httpContext.Request.Body);
			Assert.AreEqual(httpContext.Request.Body.Position, 0L);
		}
	}
}