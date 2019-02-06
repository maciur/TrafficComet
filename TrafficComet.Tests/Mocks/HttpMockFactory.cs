using Microsoft.AspNetCore.Http;
using Moq;
using System.IO;
using System.Net;

namespace TrafficComet.Core.Tests.Mock
{
	internal static class HttpMockFactory
	{
		internal static IHttpContextAccessor CreateIHttpContextAccessorMockObject(HttpContext mockHttpContext)
		{
			var mockObject = new Mock<IHttpContextAccessor>();
			mockObject.SetupProperty(x => x.HttpContext, mockHttpContext);
			return mockObject.Object;
		}

		internal static HttpContext CreateHttpContextMockObject(HttpRequest mockHttpRequest,
			HttpResponse mockHttpResponse, ConnectionInfo mockConnectionInfo)
		{
			var mockObject = new Mock<HttpContext>();
			mockObject.SetupGet(x => x.Request).Returns(mockHttpRequest);
			mockObject.SetupGet(x => x.Response).Returns(mockHttpResponse);
			mockObject.SetupGet(x => x.Connection).Returns(mockConnectionInfo);
			return mockObject.Object;
		}

		internal static ConnectionInfo CreateConnectionInfoMockObject(IPAddress remoteIpAddress, int remotePort,
			IPAddress localIpAddress, int localPort)
		{
			var mockObject = new Mock<ConnectionInfo>();
			mockObject.SetupProperty(x => x.RemoteIpAddress, remoteIpAddress);
			mockObject.SetupProperty(x => x.RemotePort, remotePort);
			mockObject.SetupProperty(x => x.LocalIpAddress, localIpAddress);
			mockObject.SetupProperty(x => x.LocalPort, localPort);
			return mockObject.Object;
		}

		internal static HttpResponse CreateHttpResponseMockObject(int statusCode, Stream bodyStream,
			IHeaderDictionary headers)
		{
			var mockObject = new Mock<HttpResponse>();
			mockObject.SetupProperty(x => x.StatusCode, statusCode);
			mockObject.SetupGet(x => x.Headers).Returns(headers);
			mockObject.SetupProperty(x => x.Body, bodyStream);
			mockObject.Setup(x=>x.Cookies.Append(It.IsAny<string>(), It.IsAny<string>()));
			return mockObject.Object;
		}

		internal static Mock<HttpRequest> CreateHttpRequestMockObject(PathString path, QueryString queryString,
			string method)
		{
			var mockObject = new Mock<HttpRequest>();
			mockObject.SetupProperty(x => x.Path, path);
			mockObject.SetupProperty(x => x.QueryString, queryString);
			mockObject.SetupProperty(x => x.Method, method);
			return mockObject;
		}

		internal static Mock<HttpRequest> AddHeaders(this Mock<HttpRequest> mockHttpRequest, IHeaderDictionary headers)
		{
			mockHttpRequest.SetupGet(x => x.Headers).Returns(headers);
			return mockHttpRequest;
		}

		internal static Mock<HttpRequest> AddCookies(this Mock<HttpRequest> mockHttpRequest, 
			IRequestCookieCollection cookies)
		{
			mockHttpRequest.SetupGet(x => x.Cookies).Returns(cookies);
			return mockHttpRequest;
		}

		internal static Mock<HttpRequest> AddBody(this Mock<HttpRequest> mockHttpRequest, Stream stream)
		{
			return mockHttpRequest.SetupProperty(x => x.Body, stream);
		}
	}
}