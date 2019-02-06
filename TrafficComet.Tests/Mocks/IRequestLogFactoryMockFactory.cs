using Moq;
using TrafficComet.Abstracts.Factories;
using TrafficComet.Abstracts.Logs.Request;

namespace TrafficComet.Core.Tests.Mock
{
    internal static class IRequestLogFactoryMockFactory
	{
		internal static IRequestLogFactory CreateMockObject(IRequestLog requestLog)
		{
			var mockObject = new Mock<IRequestLogFactory>();
			mockObject.Setup(x => x.Create(It.IsAny<string>())).Returns(requestLog).Verifiable();
			return mockObject.Object;
		}
	}
}