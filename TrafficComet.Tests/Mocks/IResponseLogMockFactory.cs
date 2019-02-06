using Moq;
using System.Collections.Generic;
using TrafficComet.Abstracts;
using TrafficComet.Abstracts.Logs.Response;

namespace TrafficComet.Core.Tests.Mock
{
	internal static class IResponseLogMockFactory
	{
		internal static IResponseLog CreateMockObject(string responseBody, int status,
			IDictionary<string, string> queryParams = null, IDictionary<string, string> cookies = null,
			IDictionary<string, string> headers = null, IDictionary<string, string> customParams = null)
		{
			var mockObject = new Mock<IResponseLog>();
			mockObject.SetupProperty(x => x.Body, responseBody);
			mockObject.SetupProperty(x => x.Status, status);
			mockObject.SetupProperty(x => x.Headers, headers);
			mockObject.SetupProperty(x => x.CustomParams, customParams);
			return mockObject.Object;
		}
	}
}