using Moq;
using System.Collections.Generic;
using TrafficComet.Abstracts.Logs.Request;

namespace TrafficComet.Core.Tests.Mock
{
    internal static class IRequestLogMockFactory
	{
		internal static IRequestLog CreateMockObject(string requestPath, string requestBody, string httpMethod,
			IDictionary<string, string> queryParams = null, IDictionary<string, string> cookies = null,
			IDictionary<string, string> headers = null, IDictionary<string, string> customParams = null)
		{
			var mockObject = new Mock<IRequestLog>();
			mockObject.SetupProperty(x => x.Path, requestPath);
			mockObject.SetupProperty(x => x.Body, requestBody);
			mockObject.SetupProperty(x => x.HttpMethod, httpMethod);
			mockObject.SetupProperty(x => x.QueryParams, queryParams);
			mockObject.SetupProperty(x => x.Cookies, cookies);
			mockObject.SetupProperty(x => x.Headers, headers);
			mockObject.SetupProperty(x => x.CustomParams, customParams);
			return mockObject.Object;
		}
	}
}