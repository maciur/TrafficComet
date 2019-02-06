using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using TrafficComet.Core.Configs;
using TrafficComet.Core.Middlewares;
using TrafficComet.Core.Tests.Mock;
using TrafficComet.Core.Tests.Tests.Mocks;

namespace TrafficComet.Core.Tests.Mocks
{
	internal static class RequestReadMiddlewareMockFactory
	{
		internal static RequestReadMiddleware CreateMockObject(out HttpContext httpContext, RequestReadMiddlewareConfig config = null,
			string requestPath = null)
		{
			var mockHttpRequest = HttpMockFactory.CreateHttpRequestMockObject(new PathString(requestPath ?? MockStaticData.RequestPath),
				new QueryString(), MockStaticData.HttpMethodPost);

			var requestMemoryStream = new MemoryStream(Encoding.UTF8.GetBytes(MockStaticData.RequestBody));

			mockHttpRequest.AddBody(requestMemoryStream);

			var httpResponse = HttpMockFactory.CreateHttpResponseMockObject(200, new MemoryStream(Encoding.UTF8.GetBytes(MockStaticData.ResponseBody)), MockStaticData.RequestHeaders);

			var connectionInfo = HttpMockFactory.CreateConnectionInfoMockObject(MockStaticData.IPAddress1,
				MockStaticData.Port1, MockStaticData.IPAddress2, MockStaticData.Port2);

			httpContext = HttpMockFactory.CreateHttpContextMockObject(mockHttpRequest.Object, httpResponse, connectionInfo);

			var middlewareConfig = config ?? new RequestReadMiddlewareConfig();

			return new RequestReadMiddleware(MockServicesHelper.FakeRequestDelegate,
				MockServicesHelper.CreateOptionsMock(middlewareConfig));
		}
	}
}