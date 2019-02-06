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
	public class ResponeReadMiddleware_Tests
	{
		[Test]
		public async Task ResponseBodyReadSuccessfully()
		{
			var accessor = new TrafficCometMiddlewaresAccessor();
			var bodyReader = IStringBodyReaderMockFactory.CreateMockObject(MockStaticData.ResponseBody);

			var requestReadMiddleware = ResponseReadMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);
			await requestReadMiddleware.Invoke(httpContext, accessor, bodyReader);

			Assert.Null(accessor.RequestBody);
			Assert.AreEqual(accessor.ResponseBody, MockStaticData.ResponseBody);
			Assert.NotNull(httpContext.Response.Body);
			Assert.AreEqual(httpContext.Response.Body.Position, 0L);
		}

		[Test]
		public void ThrowException_NullBodyReader()
		{
			var requestReadMiddleware = ResponseReadMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);

			Assert.ThrowsAsync<ArgumentNullException>(() =>
				requestReadMiddleware.Invoke(httpContext, new TrafficCometMiddlewaresAccessor(), null));
		}

		[Test]
		public void ThrowException_NullTrafficCometAccessor()
		{
			var requestReadMiddleware = ResponseReadMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);
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
			Assert.IsNull(accessor.ResponseBody);
			Assert.NotNull(httpContext.Response.Body);
			Assert.AreEqual(httpContext.Response.Body.Position, 0L);
		}

		[Test]
		public async Task IgnoredRequest()
		{
			var middlewareConfig = new ResponseReadMiddlewareConfig
			{
				IgnoreUrls = new[] { MockStaticData.RequestPath }
			};
			await RunMiddlewareWithConfiguration(middlewareConfig);
		}

		[Test]
		public async Task StopLogging()
		{
			var middlewareConfig = new ResponseReadMiddlewareConfig
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

			var middlewareConfig = new ResponseReadMiddlewareConfig
			{
				StartLoggingFileRequest = false
			};

			var requestReadMiddleware = ResponseReadMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext,
				middlewareConfig, "/bootstrap.css");

			await requestReadMiddleware.Invoke(httpContext, accessor, bodyReader);

			Assert.IsNull(accessor.RequestBody);
			Assert.IsNull(accessor.ResponseBody);
			Assert.NotNull(httpContext.Response.Body);
			Assert.AreEqual(httpContext.Response.Body.Position, 0L);
		}

		[Ignore("This is only helper method")]
		internal async Task RunMiddlewareWithConfiguration(ResponseReadMiddlewareConfig config)
		{
			var accessor = new TrafficCometMiddlewaresAccessor();
			var bodyReader = IStringBodyReaderMockFactory.CreateMockObject(MockStaticData.RequestBody);

			var requestReadMiddleware = ResponseReadMiddlewareMockFactory.CreateMockObject(
				out HttpContext httpContext, config);

			await requestReadMiddleware.Invoke(httpContext, accessor, bodyReader);

			Assert.IsNull(accessor.RequestBody);
			Assert.IsNull(accessor.ResponseBody);
			Assert.NotNull(httpContext.Response.Body);
			Assert.AreEqual(httpContext.Response.Body.Position, 0L);
		}
	}
}