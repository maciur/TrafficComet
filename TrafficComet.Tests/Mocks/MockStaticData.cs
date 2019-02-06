using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System.Collections.Generic;
using System.Net;
using TrafficComet.Core.Configs;
using TrafficComet.Core.Consts;

namespace TrafficComet.Core.Tests.Tests.Mocks
{
    internal static class MockStaticData
	{
		internal const string RequestPath = "/test-path";
		internal const string HttpMethodGet = "GET";
		internal const string HttpMethodPost = "POST";
		internal const string HttpMethodPut = "PUT";

		internal const string RequestBody = "{ \"request\":\"test-request-value\"}";
		internal const string ResponseBody = "{ \"response\":\"test-response-value\"}";

		internal const string TraceId = "f08a862c-54e6-489f-853c-ef2aafa46dec";
		internal const string UserUniqueId = "899d3e3a-2691-4f45-9a50-bc81397d8d58";

		internal readonly static IPAddress IPAddress1 = IPAddress.Parse("192.168.135.120");
		internal readonly static IPAddress IPAddress2 = IPAddress.Parse("192.168.135.130");
		internal const int Port1 = 1111;
		internal const int Port2 = 2222;

		internal const string MachineName = "Test-Machine-Name";
		internal const string ApplicationId = "test-app-id-1234";

		internal const string RequestHeaderToIgnore = "Test-Request-Header-To-Ignore";
		internal const string RequestCookieToIgnore = "test-request-cookie-to-ignore";

		internal const string ResponseHeaderToIgnore = "Test-Response-Header-To-Ignore";

		internal readonly static IDictionary<string, string> CustomParams = new Dictionary<string, string>
		{
			{ "custom-param", "test-custom-value" }
		};

		internal readonly static IDictionary<string, string> RequestCustomParams = new Dictionary<string, string>
		{
			{ "custom-param", "test-request-custom-value" }
		};

		internal readonly static IDictionary<string, string> ResponseCustomParams = new Dictionary<string, string>
		{
			{ "custom-param", "test-response-custom-value" }
		};

		internal readonly static IHeaderDictionary RequestHeaders = new HeaderDictionary
		{
			{ "Test-Header", "Test-Header-Value" },
			{ RequestHeaderToIgnore, "Test-Header-Value" }
		};

		internal readonly static IHeaderDictionary RequestHeadersWithTrackIds = new HeaderDictionary
		{
			{ "Test-Header", "Test-Header-Value" },
			{ TrafficCometConstValues.TraceIdHeader, TraceId },
			{ TrafficCometConstValues.ClientUniqueIdHeader, UserUniqueId }
		};

		internal readonly static IRequestCookieCollection RequestCookies =
			new RequestCookieCollection(new Dictionary<string, string>
			{
				{ "test-cookie", "request-test-cookie-value" },
				{ RequestCookieToIgnore, "request-test-cookie-value-to-ignore" }
			});

		internal readonly static IRequestCookieCollection RequestCookiesWithTrackIds =
			new RequestCookieCollection(new Dictionary<string, string>
			{
				{ TrafficCometConstValues.DefaultClientUniqueIdCookieName, UserUniqueId },
			});

		internal readonly static IHeaderDictionary ResponseHeaders = new HeaderDictionary
		{
			{ "Content-Type", "application/json; charset=utf-8" }
		};

		internal readonly static RequestLogFactoryConfig RequestLogFactoryConfig = new RequestLogFactoryConfig();
		internal readonly static ResponseLogFactoryConfig ResponseLogFactoryConfig = new ResponseLogFactoryConfig();

		internal readonly static RequestLogFactoryConfig RequestLogFactoryConfig_Ignore = new RequestLogFactoryConfig
		{
			IgnoreCookies = new[] { RequestCookieToIgnore },
			IgnoreHeaders = new[] { RequestHeaderToIgnore }
		};

		internal readonly static ResponseLogFactoryConfig ResponseLogFactoryConfig_Ignore = new ResponseLogFactoryConfig
		{
			IgnoreHeaders = new[] { ResponseHeaderToIgnore }
		};

		internal readonly static TrafficCometLog MockLog = new TrafficCometLog
		{
			ApplicationId = ApplicationId,
		};
	}
}