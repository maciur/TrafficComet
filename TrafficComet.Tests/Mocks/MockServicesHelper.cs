using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using TrafficComet.Abstracts.Accessors;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Abstracts.Logs.Request;
using TrafficComet.Abstracts.Logs.Response;
using TrafficComet.Abstracts.Readers;
using TrafficComet.Abstractss.Factories;
using TrafficComet.Core.Configs;
using TrafficComet.Core.Factories;
using TrafficComet.Core.Tests.Tests.Mocks;

namespace TrafficComet.Core.Tests.Mock
{
    internal static class MockServicesHelper
    {
        internal static TrafficLogFactory CreateTrafficLog(IRequestLog requestLog = null, IResponseLog responseLog = null,
            IRequestLogFactory requestLogFactory = null, IResponseLogFactory responseLogFactory = null,
            IIpAddressReader ipAddressReader = null, IMachineNameReader machineNameReader = null,
            IDatesTrafficFactory datesTrafficFactory = null, IClientLogFactory clientLogFactory = null,
            IServerLogFactory serverLogFactory = null)
        {
            if (requestLog == null)
            {
                requestLog = IRequestLogMockFactory.CreateMockObject(MockStaticData.RequestPath,
                MockStaticData.RequestBody, MockStaticData.HttpMethodGet);
            }

            if (requestLogFactory == null)
            {
                requestLogFactory = IRequestLogFactoryMockFactory.CreateMockObject(requestLog);
            }

            if (responseLog == null)
            {
                responseLog = IResponseLogMockFactory.CreateMockObject(MockStaticData.ResponseBody, 200);
            }

            if (responseLogFactory == null)
            {
                responseLogFactory = IResponseLogFactoryMockFactory.CreateMOckObject(responseLog);
            }

            if (machineNameReader == null)
            {
                machineNameReader = IMachineNameReaderMockFactory.CreateMockObject(MockStaticData.MachineName);
            }

            if (ipAddressReader == null)
            {
                ipAddressReader = IIpAddressReaderMockFactory.CreateMockObject($"{MockStaticData.IPAddress1}:{MockStaticData.Port1}",
                $"{MockStaticData.IPAddress2}:{MockStaticData.Port2}");
            }

            return new TrafficLogFactory(requestLogFactory, responseLogFactory, datesTrafficFactory, clientLogFactory, serverLogFactory, null);
        }

        internal static IHttpContextAccessor CreateHttpContextAccessor(string httpMetod = MockStaticData.HttpMethodGet, IHeaderDictionary requestHeaders = null, IRequestCookieCollection requestCookie = null)
        {
            var mockHttpRequest = HttpMockFactory.CreateHttpRequestMockObject(new PathString(MockStaticData.RequestPath),
                new QueryString(), httpMetod);

            mockHttpRequest.AddHeaders(requestHeaders ?? MockStaticData.RequestHeaders);
            mockHttpRequest.AddCookies(requestCookie ?? MockStaticData.RequestCookies);

            var mockHttpResponse = HttpMockFactory.CreateHttpResponseMockObject(200, new MemoryStream(), MockStaticData.ResponseHeaders);

            var mockConnectionInfo = HttpMockFactory.CreateConnectionInfoMockObject(MockStaticData.IPAddress1, MockStaticData.Port1,
                MockStaticData.IPAddress2, MockStaticData.Port2);

            var mockHttpContext = HttpMockFactory.CreateHttpContextMockObject(mockHttpRequest.Object, mockHttpResponse, mockConnectionInfo);

            return HttpMockFactory.CreateIHttpContextAccessorMockObject(mockHttpContext);
        }

        internal static RequestLogFactory CreateRequestLogFactory(IHttpContextAccessor httpContextAccessor = null,
            RequestLogFactoryConfig requestLogFactoryConfig = null, ITrafficCometMiddlewaresAccessor trafficCometMiddlewaresAccessor = null)
        {
            if (httpContextAccessor == null)
            {
                httpContextAccessor = CreateHttpContextAccessor();
            }

            var options = CreateOptionsForRequestLogFactory(requestLogFactoryConfig);

            if (trafficCometMiddlewaresAccessor == null)
            {
                trafficCometMiddlewaresAccessor = ITrafficCometMiddlewaresAccessorMockFactory.CreateObjectMock(
                    requestCustomParams: MockStaticData.RequestCustomParams,
                    responseCustomParams: MockStaticData.ResponseCustomParams);
            }

            var requestLogFactory = new RequestLogFactory(httpContextAccessor, options, trafficCometMiddlewaresAccessor);
            return requestLogFactory;
        }

        internal static ResponseLogFactory CreateResponseLogFactory(IHttpContextAccessor httpContextAccessor = null,
            ResponseLogFactoryConfig responseLogFactoryConfig = null, ITrafficCometMiddlewaresAccessor trafficCometMiddlewaresAccessor = null)
        {
            if (httpContextAccessor == null)
            {
                httpContextAccessor = CreateHttpContextAccessor();
            }

            var options = CreateOptionsForResponseLogFactory(responseLogFactoryConfig);

            if (trafficCometMiddlewaresAccessor == null)
            {
                trafficCometMiddlewaresAccessor = ITrafficCometMiddlewaresAccessorMockFactory.CreateObjectMock(
                    requestCustomParams: MockStaticData.RequestCustomParams,
                    responseCustomParams: MockStaticData.ResponseCustomParams);
            }

            var requestLogFactory = new ResponseLogFactory(httpContextAccessor, options, trafficCometMiddlewaresAccessor);
            return requestLogFactory;
        }

        internal static IOptions<RequestLogFactoryConfig> CreateOptionsForRequestLogFactory(RequestLogFactoryConfig requestLogFactoryConfig = null)
        {
            return CreateOptionsMock(requestLogFactoryConfig ?? MockStaticData.RequestLogFactoryConfig);
        }

        internal static IOptions<ResponseLogFactoryConfig> CreateOptionsForResponseLogFactory(ResponseLogFactoryConfig responseLogFactoryConfig = null)
        {
            return CreateOptionsMock(responseLogFactoryConfig ?? MockStaticData.ResponseLogFactoryConfig);
        }

        internal static IOptions<TOptions> CreateOptionsMock<TOptions>(TOptions options)
            where TOptions : class, new()
        {
            var mockOptions = new Mock<IOptions<TOptions>>();
            mockOptions.SetupGet(x => x.Value).Returns(options);
            return mockOptions.Object;
        }

        internal static Task FakeRequestDelegate(HttpContext context)
        {
            return Task.Factory.StartNew(() => { });
        }
    }
}