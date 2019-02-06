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
	public class TrafficCometMiddleware_Tests
	{
		[Test]
		public async Task SuccessfullyRun()
		{
			var accessor = new TrafficCometMiddlewaresAccessor();
			var logWriter = ITrafficLogWriterMockFactory.CreateMockObject();

			var trafficCometMiddleware = TrafficCometMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);
			var trafficLogFactory = ITrafficLogFactoryMockFactory.CreateMockObject(MockStaticData.MockLog);
			var traceIdGenerator = ITraceIdGeneratorMockFactory.CreateObjectMock(MockStaticData.TraceId);
			var clientIdGenerator = IClientUniqueIdGeneratorMockFactory.CreateMockObject(MockStaticData.UserUniqueId);

			await trafficCometMiddleware.Invoke(httpContext, accessor, logWriter, trafficLogFactory, traceIdGenerator, clientIdGenerator);

			Assert.Null(accessor.ResponseBody);
			Assert.Null(accessor.RequestBody);
			Assert.False(accessor.IgnoreWholeRequest);
			Assert.AreEqual(accessor.TraceId, MockStaticData.TraceId);
			Assert.AreEqual(accessor.ClientUniqueId, MockStaticData.UserUniqueId);
			Assert.NotNull(httpContext.Response.Body);
			Assert.AreEqual(httpContext.Response.Body.Position, 0L);
			Assert.NotNull(httpContext.Request.Body);
			Assert.AreEqual(httpContext.Request.Body.Position, 0L);
		}

		[Test]
		public void ThrowException_NullMiddlewaresAccessor()
		{
			var logWriter = ITrafficLogWriterMockFactory.CreateMockObject();

			var requestReadMiddleware = TrafficCometMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);
			var trafficLogFactory = ITrafficLogFactoryMockFactory.CreateMockObject(MockStaticData.MockLog);
			var traceIdGenerator = ITraceIdGeneratorMockFactory.CreateObjectMock(MockStaticData.TraceId);
			var clientIdGenerator = IClientUniqueIdGeneratorMockFactory.CreateMockObject(MockStaticData.UserUniqueId);

			Assert.ThrowsAsync<ArgumentNullException>(() =>
				requestReadMiddleware.Invoke(httpContext, null, 
				logWriter, trafficLogFactory, traceIdGenerator, clientIdGenerator));
		}

		[Test]
		public void ThrowException_NullLogWritter()
		{
			var accessor = new TrafficCometMiddlewaresAccessor();

			var requestReadMiddleware = TrafficCometMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);
			var trafficLogFactory = ITrafficLogFactoryMockFactory.CreateMockObject(MockStaticData.MockLog);
			var traceIdGenerator = ITraceIdGeneratorMockFactory.CreateObjectMock(MockStaticData.TraceId);
			var clientIdGenerator = IClientUniqueIdGeneratorMockFactory.CreateMockObject(MockStaticData.UserUniqueId);

			Assert.ThrowsAsync<ArgumentNullException>(() =>
				requestReadMiddleware.Invoke(httpContext, accessor,
				null, trafficLogFactory, traceIdGenerator, clientIdGenerator));
		}

		[Test]
		public void ThrowException_NullLogFactory()
		{
			var accessor = new TrafficCometMiddlewaresAccessor();
			var logWriter = ITrafficLogWriterMockFactory.CreateMockObject();

			var requestReadMiddleware = TrafficCometMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);
			var traceIdGenerator = ITraceIdGeneratorMockFactory.CreateObjectMock(MockStaticData.TraceId);
			var clientIdGenerator = IClientUniqueIdGeneratorMockFactory.CreateMockObject(MockStaticData.UserUniqueId);

			Assert.ThrowsAsync<ArgumentNullException>(() =>
				requestReadMiddleware.Invoke(httpContext, accessor,
				logWriter, null, traceIdGenerator, clientIdGenerator));
		}

		[Test]
		public void ThrowException_NullTraceIdGenerator()
		{
			var accessor = new TrafficCometMiddlewaresAccessor();
			var logWriter = ITrafficLogWriterMockFactory.CreateMockObject();

			var requestReadMiddleware = TrafficCometMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);
			var trafficLogFactory = ITrafficLogFactoryMockFactory.CreateMockObject(MockStaticData.MockLog);
			var clientIdGenerator = IClientUniqueIdGeneratorMockFactory.CreateMockObject(MockStaticData.UserUniqueId);

			Assert.ThrowsAsync<ArgumentNullException>(() =>
				requestReadMiddleware.Invoke(httpContext, accessor,
				logWriter, trafficLogFactory, null, clientIdGenerator));
		}

		[Test]
		public void ThrowException_NullClientIdGenerator()
		{
			var accessor = new TrafficCometMiddlewaresAccessor();
			var logWriter = ITrafficLogWriterMockFactory.CreateMockObject();

			var requestReadMiddleware = TrafficCometMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext);
			var trafficLogFactory = ITrafficLogFactoryMockFactory.CreateMockObject(MockStaticData.MockLog);
			var traceIdGenerator = ITraceIdGeneratorMockFactory.CreateObjectMock(MockStaticData.TraceId);

			Assert.ThrowsAsync<ArgumentNullException>(() =>
				requestReadMiddleware.Invoke(httpContext, accessor,
				logWriter, trafficLogFactory, traceIdGenerator, null));
		}

		[Test]
		public async Task IgnoredRequest()
		{
			var middlewareConfig = new TrafficCometMiddlewareConfig
			{
				IgnoreUrls = new[] { MockStaticData.RequestPath }
			};
			await RunMiddlewareWithConfiguration(middlewareConfig);
		}

		[Test]
		public async Task StopLogging()
		{
			var middlewareConfig = new TrafficCometMiddlewareConfig
			{
				StopLogging = true
			};
			await RunMiddlewareWithConfiguration(middlewareConfig);
		}

		[Test]
		public async Task StopLoggingFileRequests()
		{
			var middlewareConfig = new TrafficCometMiddlewareConfig
			{
				StartLoggingFileRequest = false
			};
			await RunMiddlewareWithConfiguration(middlewareConfig, "/bootstrap.css");
		}

		[Ignore("This is only helper method")]
		internal async Task RunMiddlewareWithConfiguration(TrafficCometMiddlewareConfig config, string requestPath = null)
		{
			var accessor = new TrafficCometMiddlewaresAccessor();
			var logWriter = ITrafficLogWriterMockFactory.CreateMockObject();

			var trafficCometMiddleware = TrafficCometMiddlewareMockFactory.CreateMockObject(out HttpContext httpContext, config, requestPath);
			var trafficLogFactory = ITrafficLogFactoryMockFactory.CreateMockObject(MockStaticData.MockLog);
			var traceIdGenerator = ITraceIdGeneratorMockFactory.CreateObjectMock(MockStaticData.TraceId);
			var clientIdGenerator = IClientUniqueIdGeneratorMockFactory.CreateMockObject(MockStaticData.UserUniqueId);

			await trafficCometMiddleware.Invoke(httpContext, accessor, logWriter, trafficLogFactory, traceIdGenerator, clientIdGenerator);
			
			Assert.True(accessor.IgnoreWholeRequest);
		}
	}
}