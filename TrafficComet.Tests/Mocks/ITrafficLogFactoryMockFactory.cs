using Moq;
using TrafficComet.Abstracts.Logs;
using TrafficComet.Abstractss.Factories;

namespace TrafficComet.Core.Tests.Mock
{
    internal static class ITrafficLogFactoryMockFactory
    {
        internal static ITrafficLogFactory CreateMockObject(ITrafficLog trafficLog, bool ignoreRequest = false, bool ignoreResponse = false)
        {
            var mockObject = new Mock<ITrafficLogFactory>();
            mockObject.Setup(x => x.Create(It.IsAny<ITrafficLogParams>(), ignoreRequest, ignoreResponse))
                .Returns(trafficLog).Verifiable();
            return mockObject.Object;
        }
    }
}