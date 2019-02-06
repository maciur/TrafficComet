using Moq;
using System.Collections.Generic;
using TrafficComet.Abstracts.Accessors;

namespace TrafficComet.Core.Tests.Mock
{
    internal static class ITrafficCometMiddlewaresAccessorMockFactory
    {
        internal static ITrafficCometMiddlewaresAccessor CreateObjectMock(IDictionary<string, string> customParams = null,
        IDictionary<string, string> requestCustomParams = null, IDictionary<string, string> responseCustomParams = null)
        {
            var mockObject = new Mock<ITrafficCometMiddlewaresAccessor>();
            mockObject.SetupProperty(x => x.CustomParams, customParams);
            mockObject.SetupProperty(x => x.RequestCustomParams, requestCustomParams);
            mockObject.SetupProperty(x => x.ResponseCustomParams, responseCustomParams);
            return mockObject.Object;
        }
    }
}